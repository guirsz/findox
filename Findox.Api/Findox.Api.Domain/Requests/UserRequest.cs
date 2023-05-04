using Findox.Api.Domain.Enumerators;
using System.ComponentModel.DataAnnotations;

namespace Findox.Api.Domain.Requests
{
    public class UserGetAllPaginatedRequest
    {
        public int Limit { get; set; } = 20;
        public int Offset { get; set; } = 0;
        public string FilterText { get; set; } = string.Empty;
    }

    public class UserUpdateRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        public UserRole RoleId { get; set; }

        public int[] Groups { get; set; } = new int[0];
    }

    public class UserCreateRequest
    {
        [Required]
        [StringLength(30)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(64, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;
        [Required]
        public UserRole RoleId { get; set; }

        public int[] Groups { get; set; } = new int[0];
    }
}
