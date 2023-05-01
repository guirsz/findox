namespace Findox.Api.Domain.Entities
{
    public class GrantAccessGroupEntity
    {
        public Guid DocumentId { get; set; }
        public int GroupId { get; set; }
        public DateTime GrantedDate { get; set; }
        public int GrantedBy { get; set; }
    }
}
