using Findox.Api.Domain.Responses;

namespace Findox.Api.Domain.Interfaces.Services.Group
{
    public interface IGroupGetByIdService
    {
        Task<GroupResponse?> RunAsync(int id);
    }
}
