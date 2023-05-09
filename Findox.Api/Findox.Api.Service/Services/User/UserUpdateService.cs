using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Helpers;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services.User;
using Findox.Api.Domain.Requests;

namespace Findox.Api.Service.Services.User
{
    public class UserUpdateService : IUserUpdateService
    {
        private readonly IUserRepository userRepository;
        private readonly IGroupRepository groupRepository;

        public UserUpdateService(IUserRepository userRepository, IGroupRepository groupRepository)
        {
            this.userRepository = userRepository;
            this.groupRepository = groupRepository;
        }

        public async Task<(int userId, string message)> RunAsync(UserUpdateRequest request, int requestedBy)
        {
            var userEntity = await userRepository.GetAsync(request.UserId);

            if (userEntity == null || userEntity.UserId == 0 || userEntity.Deleted)
            {
                return (0, ApplicationMessages.InvalidData);
            }

            var emailInUse = await EmailInUse(userEntity, request);
            if (emailInUse)
            {
                return (0, ApplicationMessages.EmailInUse);
            }

            bool anyInvalidGroup = await AnyInvalidGroup(request.Groups);
            if (anyInvalidGroup)
            {
                return (0, ApplicationMessages.InvalidData);
            }

            userEntity.UpdatedDate = DateTime.Now;
            userEntity.UpdatedBy = requestedBy;
            userEntity.UserName = request.UserName;
            userEntity.RoleId = request.RoleId;
            userEntity.Email = request.Email;
            userEntity.Deleted = false;

            int[] actualGroups = await userRepository.GetUserGroupsAsync(userEntity.UserId);
            var groupsToUnlink = actualGroups.Where(groupId => request.Groups.Contains(groupId) == false).ToArray();
            var groupsTolink = request.Groups.Where(groupId => actualGroups.Contains(groupId) == false).ToArray();

            await UpdateUserAsync(requestedBy, userEntity, groupsToUnlink, groupsTolink);

            return (request.UserId, ApplicationMessages.UpdatedSuccessfully);
        }

        private async Task UpdateUserAsync(int requestedBy, UserEntity userEntity, int[] groupsToUnlink, int[] groupsTolink)
        {
            await userRepository.UpdateAsync(userEntity);
            await userRepository.UnlinkGroupAsync(userEntity.UserId, groupsToUnlink);
            foreach (var groupId in groupsTolink)
            {
                await userRepository.LinkGroupAsync(new UserGroupEntity()
                {
                    UserId = userEntity.UserId,
                    GroupId = groupId,
                    GroupedBy = requestedBy,
                    GroupedDate = DateTime.Now
                });
            }
        }

        private async Task<bool> EmailInUse(UserEntity userEntity, UserUpdateRequest request)
        {
            if (userEntity.Email != request.Email)
            {
                var userNewEmail = await userRepository.FindByEmailAsync(request.Email);
                if (userNewEmail != null && userNewEmail.UserId > 0)
                {
                    return true;
                }
            }
            return false;
        }

        private async Task<bool> AnyInvalidGroup(int[] requestedGroups)
        {
            if (requestedGroups.Any())
            {
                IEnumerable<GroupEntity> groups = await groupRepository.GetManyByIdAsync(requestedGroups);
                if (groups.Count() != requestedGroups.Count())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
