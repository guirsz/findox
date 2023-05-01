namespace Findox.Api.Domain.Entities
{
    public class UserGroupEntity
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public DateTime GroupedDate { get; set; }
        public int GroupedBy { get; set; }
    }
}
