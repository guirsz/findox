namespace Findox.Api.Domain.Entities
{
    public class DocumentEntity
    {
        public Guid DocumentId { get; set; }
        public string FileName { get; set; }
        public DateTime UploadedDate { get; set; }
        public int UploadedBy { get; set; }
    }
}
