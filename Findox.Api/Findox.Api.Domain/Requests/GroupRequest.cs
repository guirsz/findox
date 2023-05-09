using System.ComponentModel.DataAnnotations;

namespace Findox.Api.Domain.Requests
{
    public class GroupGetAllPaginatedRequest
    {
        public int Limit { get; set; } = 20;
        public int Offset { get; set; } = 0;
        public string FilterText { get; set; } = string.Empty;
    }

    public class GroupUpdateRequest
    {
        [Required]
        public int GroupId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string GroupName { get; set; } = string.Empty;
    }

    public class GroupCreateRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string GroupName { get; set; } = string.Empty;
    }
}
