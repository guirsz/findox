namespace Findox.Api.Domain.Responses
{
    public class DocumentResponse
    {
        public Guid DocumentId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public int[] GrantedUsers { get; set; } = new int[0];
        public int[] GrantedGroups { get; set; } = new int[0];
    }

    public class DocumentDownloadResponse
    {
        public Guid DocumentId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public Stream FileStream { get; set; }
    }
}
