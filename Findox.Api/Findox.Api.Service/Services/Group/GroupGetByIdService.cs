using AutoMapper;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services.Group;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Service.Services.Group
{
    public class GroupGetByIdService : IGroupGetByIdService
    {
        private readonly IGroupRepository groupRepository;
        private readonly IMapper mapper;

        public GroupGetByIdService(IGroupRepository groupRepository, IMapper mapper)
        {
            this.groupRepository = groupRepository;
            this.mapper = mapper;
        }

        public async Task<GroupResponse?> RunAsync(int id)
        {
            var groupEntity = await groupRepository.GetAsync(id);
            
            if (groupEntity == null || groupEntity.GroupId == 0) return null;
            if (groupEntity.Deleted) { return null; }

            return mapper.Map<GroupResponse>(groupEntity);
        }
    }
}
