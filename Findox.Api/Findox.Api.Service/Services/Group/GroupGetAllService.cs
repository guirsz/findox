using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services.Group;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Service.Services.Group
{
    public class GroupGetAllService : IGroupGetAllService
    {
        private readonly IGroupRepository groupRepository;

        public GroupGetAllService(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task<IEnumerable<GroupResponse>> RunAsync()
        {
            return await groupRepository.GetAllAsync();
        }
    }
}
