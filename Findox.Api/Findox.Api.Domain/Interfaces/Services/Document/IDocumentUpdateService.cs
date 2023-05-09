using Findox.Api.Domain.Enumerators;
using Findox.Api.Domain.Requests;

namespace Findox.Api.Domain.Interfaces.Services.Document
{
    public interface IDocumentUpdateService
    {
        Task<(Guid documentId, string message)> RunAsync(DocumentRequest request, int requestedBy, UserRole userRole);
    }
}
