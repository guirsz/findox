using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Helpers;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Service.Services.Document;
using Moq;

namespace Findox.Api.Test.xUnit.Services.Document
{
    public class DocumentDeleteServiceTest
    {
        private Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
        private Guid documentId = Guid.NewGuid();

        [Fact]
        public async Task InvalidData()
        {
            mockDocumentRepository.Setup(a => a.GetAsync(It.IsAny<Guid>())).ReturnsAsync(() => new DocumentEntity() { DocumentId = Guid.Empty });

            var service = new DocumentDeleteService(mockDocumentRepository.Object);
            var result = await service.RunAsync(documentId, 1);

            Assert.Equal(false, result.deleted);
            Assert.Equal(ApplicationMessages.InvalidData, result.message);
        }

        [Fact]
        public async Task RemovedSuccessfullyDeleted()
        {
            mockDocumentRepository.Setup(a => a.GetAsync(It.IsAny<Guid>())).ReturnsAsync(() => new DocumentEntity() { DocumentId = documentId, Deleted = true });

            var service = new DocumentDeleteService(mockDocumentRepository.Object);
            var result = await service.RunAsync(documentId, 1);

            Assert.Equal(true, result.deleted);
            Assert.Equal(ApplicationMessages.RemovedSuccessfully, result.message);
        }

        [Fact]
        public async Task RemovedSuccessfully()
        {
            mockDocumentRepository.Setup(a => a.GetAsync(It.IsAny<Guid>())).ReturnsAsync(() => new DocumentEntity() { DocumentId = documentId, Deleted = false });
            mockDocumentRepository.Setup(a => a.UpdateAsync(It.IsAny<DocumentEntity>())).ReturnsAsync(() => true);

            var service = new DocumentDeleteService(mockDocumentRepository.Object);
            var result = await service.RunAsync(documentId, 1);

            Assert.Equal(true, result.deleted);
            Assert.Equal(ApplicationMessages.RemovedSuccessfully, result.message);
        }
    }
}
