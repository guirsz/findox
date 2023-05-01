namespace Findox.Api.Domain.Entities
{
    public class GroupEntity
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
    }
}
