using Findox.Api.Domain.Helpers;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services.Group;

namespace Findox.Api.Service.Services.Group
{
    public class GroupDeleteService : IGroupDeleteService
    {
        private readonly IGroupRepository groupRepository;

        public GroupDeleteService(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task<(bool deleted, string message)> RunAsync(int id, int requestedBy)
        {
            var groupEntity = await groupRepository.GetAsync(id);

            if (groupEntity == null || groupEntity.GroupId == 0)
            {
                return (false, ApplicationMessages.InvalidData);
            }

            if (groupEntity.Deleted == true)
            {
                return (true, ApplicationMessages.RemovedSuccessfully);
            }

            groupEntity.UpdatedDate = DateTime.Now;
            groupEntity.UpdatedBy = requestedBy;
            groupEntity.Deleted = true;

            await groupRepository.UpdateAsync(groupEntity);

            return (true, ApplicationMessages.RemovedSuccessfully);
        }
    }
}
