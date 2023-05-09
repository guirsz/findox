using System.ComponentModel.DataAnnotations;

namespace Findox.Api.Domain.Requests
{
    public class DocumentRequest
    {
        [Required]
        public Guid DocumentId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string FileName { get; set; } = string.Empty;
        public int[] GrantedUsers { get; set; } = new int[0];
        public int[] GrantedGroups { get; set; } = new int[0];
    }

    public class DocumentGetAllPaginatedRequest
    {
        public int Limit { get; set; } = 20;
        public int Offset { get; set; } = 0;
        public string FilterFileName { get; set; } = string.Empty;
    }
}
