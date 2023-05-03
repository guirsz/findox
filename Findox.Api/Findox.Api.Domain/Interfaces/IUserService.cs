using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Domain.Interfaces
{
    public interface IUserService
    {
        Task<(int userId, string message)> CreateAsync(UserRequest request, int requestedBy);
        Task<(bool deleted, string message)> DeleteAsync(int id, int requestedBy);
        Task<IEnumerable<UserResponse>> GetAllPaginatedAsync(int limit, int offset);
        Task<UserResponse?> GetByIdAsync(int userId);
        Task<(int userId, string message)> UpdateAsync(int id, UserRequest request, int requestedBy);
    }
}
