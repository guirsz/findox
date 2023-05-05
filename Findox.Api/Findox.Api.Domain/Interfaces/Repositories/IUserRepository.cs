using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Domain.Interfaces.Repositories
{
    public interface IUserRepository : ICrudRepository<UserEntity>
    {
        Task<UserEntity?> FindByEmailAsync(string email);
        Task LinkGroupAsync(UserGroupEntity userGroupEntity);
        Task<IEnumerable<UserResponse>> GetAllPaginatedAsync(UserGetAllPaginatedRequest request);
        Task<int[]> GetUserGroupsAsync(int id);
        Task UnlinkGroupAsync(int userId, IEnumerable<int> groupsToUnlink);
    }
}
