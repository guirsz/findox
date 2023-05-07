using Findox.Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.WebUtilities;

namespace Findox.Api.Service.Services
{
    public class LocalFileService : IFileService
    {
        public async Task<FileStream> GetFileAsync(string fileName)
        {
            var filePath = Path.Combine("LocalStorage", fileName);
            if (File.Exists(filePath))
            {
                return await Task.FromResult(File.Open(filePath, FileMode.Open));
            }
            return default;
        }

        public async Task SaveFileAsync(FileMultipartSection fileSection, string fileName)
        {
            Directory.CreateDirectory("LocalStorage");
            var filePath = Path.Combine("LocalStorage", fileName);
            using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
            await fileSection.FileStream.CopyToAsync(stream);
        }
    }
}
