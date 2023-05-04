using Findox.Api.Domain.Requests;

namespace Findox.Api.Domain.Interfaces.Services.User
{
    public interface IUserCreateService
    {
        Task<(int userId, string message)> RunAsync(UserCreateRequest request, int requestedBy);
    }
}
