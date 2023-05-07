using Microsoft.AspNetCore.WebUtilities;

namespace Findox.Api.Domain.Interfaces.Services.Document
{
    public interface IDocumentUploadService
    {
        Task<long> RunAsync(FileMultipartSection fileSection, string fileName, int requestedBy);
    }
}
