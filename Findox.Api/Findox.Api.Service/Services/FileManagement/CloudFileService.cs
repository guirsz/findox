using Findox.Api.Domain.Interfaces.Services.FileManagement;
using Microsoft.AspNetCore.WebUtilities;

namespace Findox.Api.Service.Services.FileManagement
{
    public class CloudFileService : IFileService
    {
        private readonly IAwsStorageService awsStorageService;

        public CloudFileService(IAwsStorageService awsStorageService)
        {
            this.awsStorageService = awsStorageService;
        }

        public async Task<FileStream?> GetFileAsync(string fileName)
        {
            var result = await awsStorageService.DownloadFileAsync(fileName);
            if (result.StatusCode == 201)
            {
                return result.OpenStream as FileStream;
            }
            return null;
        }

        public async Task<bool> SaveFileAsync(FileMultipartSection fileSection, string fileName)
        {
            MemoryStream ms = new MemoryStream();
            await fileSection.FileStream.CopyToAsync(ms);
            var result = await awsStorageService.UploadFileAsync(new Domain.Requests.S3UploadRequest()
            {
                InputStream = ms,
                Name = fileName
            });
            return result.StatusCode == 201;
        }
    }
}
