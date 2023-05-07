using Findox.Api.Domain.Enumerators;
using Findox.Api.Domain.Requests;

namespace Findox.Api.Domain.Interfaces.Services.Document
{
    public interface IDocumentUpdateDataService
    {
        Task<(Guid documentId, string message)> RunAsync(Guid id, DocumentRequest request, int requestedBy, UserRole userRole);
    }
}
