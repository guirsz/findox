using System.ComponentModel.DataAnnotations;

namespace Findox.Api.Domain.Requests
{
    public class AuthRequest
    {
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(64, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;
    }
}
