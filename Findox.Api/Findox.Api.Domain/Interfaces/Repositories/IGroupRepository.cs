using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Domain.Interfaces.Repositories
{
    public interface IGroupRepository : ICrudRepository<GroupEntity>
    {
        Task<IEnumerable<GroupResponse>> GetAllAsync();
        Task<GroupEntity> GetByName(string groupName);
        Task<IEnumerable<GroupEntity>> GetManyByIdAsync(int[] groups);
    }
}
