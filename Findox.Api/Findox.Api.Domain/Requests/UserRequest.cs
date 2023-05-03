using Findox.Api.Domain.Enumerators;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Findox.Api.Domain.Requests
{
    public class UserRequest
    {
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(30)]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        [EnumDataType(typeof(UserRole))]
        public string Role { get; set; }
    }
}
