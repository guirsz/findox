using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Responses;
using Findox.Api.Service.Services.Document;
using Moq;

namespace Findox.Api.Test.xUnit.Services.Document
{
    public class DocumentGetServiceTest
    {
        private Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
        private Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private Guid documentId = Guid.NewGuid();

        [Fact]
        public async Task DocumentNotFound()
        {
            mockDocumentRepository.Setup(a => a.GetWithGroupsAsync(It.IsAny<Guid>())).ReturnsAsync(() => new DocumentResponse() { DocumentId = Guid.Empty });

            var service = new DocumentGetService(mockDocumentRepository.Object, mockUserRepository.Object);
            var result = await service.RunAsync(documentId, 1, Domain.Enumerators.UserRole.Admin);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetDocumentFromAdmin()
        {
            mockDocumentRepository.Setup(a => a.GetWithGroupsAsync(It.IsAny<Guid>())).ReturnsAsync(() => new DocumentResponse() { DocumentId = documentId });

            var service = new DocumentGetService(mockDocumentRepository.Object, mockUserRepository.Object);
            var result = await service.RunAsync(documentId, 1, Domain.Enumerators.UserRole.Admin);

            Assert.Equal(documentId, result.DocumentId);
        }
    }
}
