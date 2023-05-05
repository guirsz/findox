using AutoMapper;
using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Helpers;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services.User;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Security;

namespace Findox.Api.Service.Services.User
{
    public class UserCreateService : IUserCreateService
    {
        private readonly IUserRepository userRepository;
        private readonly IGroupRepository groupRepository;
        private readonly IMapper mapper;

        public UserCreateService(IUserRepository userRepository, IGroupRepository groupRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.groupRepository = groupRepository;
            this.mapper = mapper;
        }

        public async Task<(int userId, string message)> RunAsync(UserCreateRequest request, int requestedBy)
        {
            var userEntity = await userRepository.FindByEmailAsync(request.Email);

            if (userEntity != null && userEntity.UserId > 0)
            {
                if (userEntity.Deleted)
                    return (0, ApplicationMessages.UserDeleted);
                else
                    return (0, ApplicationMessages.EmailUsed);
            }

            bool anyInvalidGroup = await AnyInvalidGroup(request.Groups);
            if (anyInvalidGroup)
            {
                return (0, ApplicationMessages.InvalidData);
            }

            var dateTimeNow = DateTime.Now;

            var user = mapper.Map<UserEntity>(request);
            user.PasswordSalt = Argon2Hash.CreateSalt();
            user.PasswordHash = Argon2Hash.HashPassword(request.Password, user.PasswordSalt);
            user.Deleted = true;
            user.CreatedBy = requestedBy;
            user.CreatedDate = dateTimeNow;
            user.UpdatedBy = requestedBy;
            user.UpdatedDate = dateTimeNow;

            int userId = await userRepository.InsertAsync(user);

            foreach (var groupId in request.Groups)
            {
                await userRepository.LinkGroupAsync(new UserGroupEntity()
                {
                    GroupId = groupId,
                    UserId = userId,
                    GroupedBy = requestedBy,
                    GroupedDate = dateTimeNow,
                });
            }

            return (userId, ApplicationMessages.CreatedSuccessfully);
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
