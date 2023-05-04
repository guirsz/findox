using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Domain.Interfaces.Services.User
{
    public interface IUserGetAllPaginatedService
    {
        Task<IEnumerable<UserResponse>> RunAsync(UserGetAllPaginatedRequest request);
    }
}
