namespace Findox.Api.Domain.Entities
{
    public class GrantAccessUserEntity
    {
        public Guid DocumentId { get; set; }
        public int UserId { get; set; }
        public DateTime GrantedDate { get; set; }
        public int GrantedBy { get; set; }
    }
}
