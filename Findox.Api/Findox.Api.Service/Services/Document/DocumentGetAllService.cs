using Findox.Api.Domain.Enumerators;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services.Document;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Service.Services.Document
{
    public class DocumentGetAllService : IDocumentGetAllService
    {
        private readonly IDocumentRepository documentRepository;

        public DocumentGetAllService(IDocumentRepository documentRepository)
        {
            this.documentRepository = documentRepository;
        }

        public async Task<IEnumerable<DocumentResponse>> RunAsync(DocumentGetAllPaginatedRequest request, int requestedBy, UserRole userRole)
        {
            if (userRole == UserRole.Admin)
            {
                return await documentRepository.GetAllPaginatedAsync(request, 0);
            }
            else
            {
                return await documentRepository.GetAllPaginatedAsync(request, requestedBy);
            }
        }

    }
}
