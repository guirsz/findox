using Findox.Api.Domain.Entities;

namespace Findox.Api.Domain.Interfaces.Repositories
{
    public interface IGroupRepository
    {
        Task<GroupEntity> SelectByIdAsync(int groupId);
        Task<IEnumerable<GroupEntity>> SelectManyByIdAsync(int[] groups);
    }
}
