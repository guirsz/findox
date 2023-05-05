using Findox.Api.Domain.Responses;

namespace Findox.Api.Domain.Interfaces.Services.Group
{
    public interface IGroupGetAllService
    {
        Task<IEnumerable<GroupResponse>> RunAsync();
    }
}
