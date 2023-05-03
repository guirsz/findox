using Findox.Api.Domain.Requests;

namespace Findox.Api.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<object> AuthenticateAsync(AuthRequest request);
    }
}
