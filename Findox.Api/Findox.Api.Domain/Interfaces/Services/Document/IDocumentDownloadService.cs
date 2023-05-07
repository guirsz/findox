using Findox.Api.Domain.Responses;

namespace Findox.Api.Domain.Interfaces.Services.Document
{
    public interface IDocumentDownloadService
    {
        Task<DocumentDownloadResponse> RunAsync(Guid id);
    }
}
