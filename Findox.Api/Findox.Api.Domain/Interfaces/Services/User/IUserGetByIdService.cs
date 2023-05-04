using Findox.Api.Domain.Responses;

namespace Findox.Api.Domain.Interfaces.Services.User
{
    public interface IUserGetByIdService
    {
        Task<UserResponse> RunAsync(int id);
    }
}
