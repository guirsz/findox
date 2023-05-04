using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Domain.Interfaces
{
    public interface IUserService
    {
        Task<(int userId, string message)> CreateAsync(UserCreateRequest request, int requestedBy);
        Task<(bool deleted, string message)> DeleteAsync(int id, int requestedBy);
        Task<IEnumerable<UserResponse>> GetAllPaginatedAsync(UserGetAllPaginatedRequest query);
        Task<UserResponse?> GetByIdAsync(int userId);
        Task<(int userId, string message)> UpdateAsync(int id, UserUpdateRequest request, int requestedBy);
    }
}
