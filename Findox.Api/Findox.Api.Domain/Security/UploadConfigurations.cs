namespace Findox.Api.Domain.Security
{
    public class UploadConfigurations
    {
        public long MultipartBodyLengthLimit { get; set; }
        public string AcceptedFileTypes { get; set; } = string.Empty;

        public bool FileTypeAllowed(string fileName)
        {
            if (string.IsNullOrEmpty(this.AcceptedFileTypes))
                return true;
            var validFileTypes = this.AcceptedFileTypes.ToLower().Split(",", StringSplitOptions.TrimEntries);
            return validFileTypes.Any(a => fileName.ToLower().EndsWith(a));
        }
    }
}
