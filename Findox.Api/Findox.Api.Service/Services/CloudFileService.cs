using Findox.Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.WebUtilities;

namespace Findox.Api.Service.Services
{
    public class CloudFileService : IFileService
    {
        public Task<FileStream> GetFileAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task SaveFileAsync(FileMultipartSection fileSection, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
