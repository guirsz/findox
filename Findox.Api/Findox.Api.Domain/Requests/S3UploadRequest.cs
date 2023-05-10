namespace Findox.Api.Domain.Requests
{
    public class S3UploadRequest
    {
        public string Name { get; set; } = null!;
        public MemoryStream InputStream { get; set; } = null!;
    }
}
