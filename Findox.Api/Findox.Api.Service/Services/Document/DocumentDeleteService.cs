using Findox.Api.Domain.Helpers;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services.Document;

namespace Findox.Api.Service.Services.Document
{
    public class DocumentDeleteService : IDocumentDeleteService
    {
        private readonly IDocumentRepository documentRepository;

        public DocumentDeleteService(IDocumentRepository documentRepository)
        {
            this.documentRepository = documentRepository;
        }

        public async Task<(bool deleted, string message)> RunAsync(Guid id, int requestedBy)
        {
            var documentEntity = await documentRepository.GetAsync(id);

            if (documentEntity == null || documentEntity.DocumentId == Guid.Empty)
                return (false, ApplicationMessages.InvalidData);

            if (documentEntity.Deleted)
                return (true, ApplicationMessages.RemovedSuccessfully);

            documentEntity.Deleted = true;
            documentEntity.UpdatedDate = DateTime.Now;
            documentEntity.UpdatedBy = requestedBy;

            await documentRepository.UpdateAsync(documentEntity);

            return (true, ApplicationMessages.RemovedSuccessfully);
        }
    }
}
