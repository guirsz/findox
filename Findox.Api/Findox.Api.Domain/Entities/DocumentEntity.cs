namespace Findox.Api.Domain.Entities
{
    public class DocumentEntity
    {
        public Guid DocumentId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public long FileLength { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
