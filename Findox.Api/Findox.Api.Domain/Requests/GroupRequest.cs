using System.ComponentModel.DataAnnotations;

namespace Findox.Api.Domain.Requests
{
    public class GroupRequest
    {
        [Required]
        public int GroupId { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string GroupName { get; set; } = string.Empty;
    }
}
