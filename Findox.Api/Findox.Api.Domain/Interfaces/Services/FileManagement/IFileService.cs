using Microsoft.AspNetCore.WebUtilities;

namespace Findox.Api.Domain.Interfaces.Services.FileManagement
{
    public interface IFileService
    {
        Task<bool> SaveFileAsync(FileMultipartSection fileSection, string fileName);
        Task<FileStream?> GetFileAsync(string fileName);
    }
}
