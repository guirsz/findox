using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<int> InsertAsync(UserEntity user);
        Task<UserEntity?> FindByEmailAsync(string email);
        Task LinkGroupAsync(UserGroupEntity userGroupEntity);
        Task<UserEntity> GetAsync(int id);
        Task UpdateAsync(UserEntity user);
        Task<IEnumerable<UserResponse>> GetAllPaginatedAsync(UserGetAllPaginatedRequest request);
        Task<UserResponse> GetByIdAsync(int id);
        Task<int[]> GetUserGroupsAsync(int id);
        Task UnlinkGroupAsync(int userId, IEnumerable<int> groupsToUnlink);
    }
}
