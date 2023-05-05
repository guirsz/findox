using Findox.Api.Domain.Requests;

namespace Findox.Api.Domain.Interfaces.Services.Group
{
    public interface IGroupCreateService
    {
        Task<(int groupId, string message)> RunAsync(GroupCreateRequest request, int requestedBy);
    }
}
