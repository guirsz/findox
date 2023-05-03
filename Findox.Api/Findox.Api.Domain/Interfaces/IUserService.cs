using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Domain.Interfaces
{
    public interface IUserService
    {
        Task<(int userId, string message)> Create(UserRequest request, int requestedBy);
        Task<(bool deleted, string message)> Delete(int id, int requestedBy);
        Task<IEnumerable<UserResponse>> GetAll(int limit, int offset);
        Task<UserResponse?> GetById(int userId);
        Task<(int userId, string message)> Update(int id, UserRequest request, int requestedBy);
    }
}
