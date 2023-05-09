using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services;
using Findox.Api.Domain.Interfaces.Services.Document;
using Microsoft.AspNetCore.WebUtilities;

namespace Findox.Api.Service.Services.Document
{
    public class DocumentUploadService : IDocumentUploadService
    {
        private readonly IDocumentRepository repository;
        private readonly IFileService fileService;

        public DocumentUploadService(IDocumentRepository repository, IFileService fileService)
        {
            this.repository = repository;
            this.fileService = fileService;
        }

        public async Task<long> RunAsync(FileMultipartSection fileSection, string fileName, int requestedBy)
        {
            var documentId = Guid.NewGuid();

            await fileService.SaveFileAsync(fileSection, documentId.ToString());

            await repository.InsertAsync(new DocumentEntity()
            {
                DocumentId = documentId,
                FileName = fileName,
                FileLength = fileSection.FileStream.Length,
                CreatedBy = requestedBy,
                CreatedDate = DateTime.Now,
                Deleted = false
            });

            await repository.GrantAccessToUserAsync(new GrantAccessUserEntity()
            {
                UserId = requestedBy,
                DocumentId = documentId,
                GrantedDate = DateTime.Now,
                GrantedBy = requestedBy
            });

            return fileSection.FileStream.Length;
        }
    }
}
