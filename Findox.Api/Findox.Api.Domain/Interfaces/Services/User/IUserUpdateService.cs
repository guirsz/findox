using Findox.Api.Domain.Requests;

namespace Findox.Api.Domain.Interfaces.Services.User
{
    public interface IUserUpdateService
    {
        Task<(int userId, string message)> RunAsync(int id, UserUpdateRequest request, int requestedBy);
    }
}
