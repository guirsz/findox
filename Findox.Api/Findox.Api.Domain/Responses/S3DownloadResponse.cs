namespace Findox.Api.Domain.Responses
{
    public class S3DownloadResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public Stream OpenStream { get; set; } = null!;
    }
}
