using Findox.Api.Domain.Requests;

namespace Findox.Api.Domain.Interfaces.Services.Group
{
    public interface IGroupUpdateService
    {
        Task<(int groupId, string message)> RunAsync(GroupUpdateRequest request, int requestedBy);
    }
}
