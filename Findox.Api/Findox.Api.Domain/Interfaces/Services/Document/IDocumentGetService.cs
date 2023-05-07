using Findox.Api.Domain.Enumerators;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Domain.Interfaces.Services.Document
{
    public interface IDocumentGetService
    {
        Task<DocumentResponse?> RunAsync(Guid id, int requestedBy, UserRole userRole);
    }
}
