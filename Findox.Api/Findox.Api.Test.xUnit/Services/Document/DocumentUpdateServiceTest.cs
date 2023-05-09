using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Enumerators;
using Findox.Api.Domain.Helpers;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services.Document;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;
using Findox.Api.Domain.Security;
using Findox.Api.Service.Services.Document;
using Moq;

namespace Findox.Api.Test.xUnit.Services.Document
{
    public class DocumentUpdateServiceTest
    {
        private Mock<IDocumentGetService> mockDocumentGetService = new Mock<IDocumentGetService>();
        private Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
        private UploadConfigurations uploadConfigurations = new UploadConfigurations()
        {
            AcceptedFileTypes = "pdf"
        };
        private Guid documentId = Guid.NewGuid();

        [Fact]
        public async Task EmptyFileName()
        {
            var service = new DocumentUpdateService(mockDocumentGetService.Object, mockDocumentRepository.Object, uploadConfigurations);
            var result = await service.RunAsync(new DocumentRequest() { FileName = string.Empty }, 1, default);

            Assert.Equal(Guid.Empty, result.documentId);
            Assert.Equal(ApplicationMessages.InvalidData, result.message);
        }

        [Fact]
        public async Task FileTypeNotAllowed()
        {
            var service = new DocumentUpdateService(mockDocumentGetService.Object, mockDocumentRepository.Object, uploadConfigurations);
            var result = await service.RunAsync(new DocumentRequest() { FileName = "file.ppt" }, 1, default);

            Assert.Equal(Guid.Empty, result.documentId);
            Assert.Equal(ApplicationMessages.InvalidFileName, result.message);
        }

        [Fact]
        public async Task DocumentNotFound()
        {
            mockDocumentGetService.Setup(a => a.RunAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<UserRole>())).ReturnsAsync(() => null);

            var service = new DocumentUpdateService(mockDocumentGetService.Object, mockDocumentRepository.Object, uploadConfigurations);
            var result = await service.RunAsync(new DocumentRequest() { FileName = "file.pdf" }, 1, default);

            Assert.Equal(Guid.Empty, result.documentId);
            Assert.Equal(ApplicationMessages.InvalidData, result.message);
        }

        [Fact]
        public async Task UpdateDocument()
        {
            mockDocumentGetService
                .Setup(a => a.RunAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<UserRole>()))
                .ReturnsAsync(() => new DocumentResponse() { DocumentId = documentId });

            mockDocumentRepository.Setup(a => a.GetAsync(It.IsAny<Guid>())).ReturnsAsync(() => new DocumentEntity() { DocumentId = documentId });
            mockDocumentRepository.Setup(a => a.UpdateAsync(It.IsAny<DocumentEntity>()));

            var service = new DocumentUpdateService(mockDocumentGetService.Object, mockDocumentRepository.Object, uploadConfigurations);
            var result = await service.RunAsync(new DocumentRequest() { DocumentId = documentId, FileName = "file.pdf" }, 1, default);

            Assert.Equal(documentId, result.documentId);
            Assert.Equal(ApplicationMessages.UpdatedSuccessfully, result.message);
        }
    }
}
