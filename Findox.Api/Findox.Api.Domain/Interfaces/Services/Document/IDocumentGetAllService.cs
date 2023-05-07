using Findox.Api.Domain.Enumerators;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Domain.Interfaces.Services.Document
{
    public interface IDocumentGetAllService
    {
        Task<IEnumerable<DocumentResponse>> RunAsync(DocumentGetAllPaginatedRequest request, int requestedBy, UserRole userRole);
    }
}
