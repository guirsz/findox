using System.ComponentModel.DataAnnotations;

namespace Findox.Api.Domain.Requests
{
    public class AuthRequest
    {
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string Password { get; set; }
    }
}
