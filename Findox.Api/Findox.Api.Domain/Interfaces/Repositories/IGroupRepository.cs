using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Domain.Interfaces.Repositories
{
    public interface IGroupRepository : ICrudRepository<GroupEntity>
    {
        Task<GroupResponse[]> GetAllAsync();
        Task<GroupEntity> GetByNameAsync(string groupName);
        Task<int> GetCountAsync(int[] groups);
    }
}
