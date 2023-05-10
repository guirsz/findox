using Findox.Api.Domain.Interfaces.Services.FileManagement;
using Microsoft.AspNetCore.WebUtilities;

namespace Findox.Api.Service.Services.FileManagement
{
    public class LocalFileService : IFileService
    {
        public async Task<FileStream?> GetFileAsync(string fileName)
        {
            var filePath = Path.Combine("LocalStorage", fileName);
            if (File.Exists(filePath))
            {
                return await Task.FromResult(File.Open(filePath, FileMode.Open));
            }
            return null;
        }

        public async Task<bool> SaveFileAsync(FileMultipartSection fileSection, string fileName)
        {
            Directory.CreateDirectory("LocalStorage");
            var filePath = Path.Combine("LocalStorage", fileName);
            using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
            await fileSection.FileStream.CopyToAsync(stream);
            return true;
        }
    }
}
