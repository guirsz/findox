using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Helpers;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services.Group;
using Findox.Api.Domain.Requests;

namespace Findox.Api.Service.Services.Group
{
    public class GroupCreateService : IGroupCreateService
    {
        private readonly IGroupRepository groupRepository;

        public GroupCreateService(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task<(int groupId, string message)> RunAsync(GroupCreateRequest request, int requestedBy)
        {
            var groupEntity = await groupRepository.GetByNameAsync(request.GroupName);

            if (groupEntity != null && !groupEntity.Deleted)
            {
                return (0, ApplicationMessages.GroupAlreadyExists);
            }

            if (groupEntity != null && groupEntity.Deleted)
            {
                groupEntity.Deleted = false;
                groupEntity.UpdatedBy = requestedBy;
                groupEntity.UpdatedDate = DateTime.Now;
                await groupRepository.UpdateAsync(groupEntity);
                return (groupEntity.GroupId, ApplicationMessages.CreatedSuccessfully);
            }

            int groupId = await groupRepository.InsertAsync(new GroupEntity()
            {
                GroupName = request.GroupName,
                CreatedBy = requestedBy,
                CreatedDate = DateTime.Now,
            });

            return (groupId, ApplicationMessages.CreatedSuccessfully);
        }
    }
}
