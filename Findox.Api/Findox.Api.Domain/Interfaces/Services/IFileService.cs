using Microsoft.AspNetCore.WebUtilities;

namespace Findox.Api.Domain.Interfaces.Services
{
    public interface IFileService
    {
        Task SaveFileAsync(FileMultipartSection fileSection, string fileName);
        Task<FileStream> GetFileAsync(string fileName);
    }
}
