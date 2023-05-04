using Findox.Api.Domain.Requests;

namespace Findox.Api.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<object> RunAsync(AuthRequest request);
    }
}
