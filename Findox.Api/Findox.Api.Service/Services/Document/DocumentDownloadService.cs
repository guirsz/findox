using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services;
using Findox.Api.Domain.Interfaces.Services.Document;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Service.Services.Document
{
    public class DocumentDownloadService : IDocumentDownloadService
    {
        private readonly IDocumentRepository documentRepository;
        private readonly IFileService fileService;

        public DocumentDownloadService(IDocumentRepository documentRepository, IFileService fileService)
        {
            this.documentRepository = documentRepository;
            this.fileService = fileService;
        }

        public async Task<DocumentDownloadResponse> RunAsync(Guid id)
        {
            var documentEntity = await documentRepository.GetAsync(id);

            if (documentEntity == null || documentEntity.DocumentId == Guid.Empty)
            {
                return null;
            }

            var streamResult = await fileService.GetFileAsync(documentEntity.DocumentId.ToString());

            return new DocumentDownloadResponse()
            {
                DocumentId = documentEntity.DocumentId,
                FileName = documentEntity.FileName,
                FileStream = streamResult
            };
        }
    }
}
