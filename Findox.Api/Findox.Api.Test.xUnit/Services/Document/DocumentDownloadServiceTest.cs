using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services.FileManagement;
using Findox.Api.Service.Services.Document;
using Moq;

namespace Findox.Api.Test.xUnit.Services.Document
{
    public class DocumentDownloadServiceTest
    {
        private Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
        private Mock<IFileService> mockFileService = new Mock<IFileService>();
        private Guid documentId = Guid.NewGuid();
        private Mock<FileStream> mockFileStream = new Mock<FileStream>();

        [Fact]
        public async Task DocumentNotFound()
        {
            mockDocumentRepository.Setup(a => a.GetAsync(It.IsAny<Guid>())).ReturnsAsync(() => new DocumentEntity() { DocumentId = Guid.Empty });

            var service = new DocumentDownloadService(mockDocumentRepository.Object, mockFileService.Object);
            var result = await service.RunAsync(documentId);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetDocumentToDownload()
        {
            mockDocumentRepository.Setup(a => a.GetAsync(It.IsAny<Guid>())).ReturnsAsync(() => new DocumentEntity() { DocumentId = documentId, FileName = "file1" });
            mockFileService.Setup(a => a.GetFileAsync(It.IsAny<string>())).ReturnsAsync(() => It.IsAny<FileStream>());

            var service = new DocumentDownloadService(mockDocumentRepository.Object, mockFileService.Object);
            var result = await service.RunAsync(documentId);

            Assert.Equal("file1", result.FileName);
        }
    }
}
