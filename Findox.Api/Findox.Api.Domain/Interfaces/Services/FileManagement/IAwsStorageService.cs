using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Domain.Interfaces.Services.FileManagement
{
    public interface IAwsStorageService
    {
        Task<S3UploadResponse> UploadFileAsync(S3UploadRequest request);
        Task<S3DownloadResponse> DownloadFileAsync(string fileName);
    }
}
