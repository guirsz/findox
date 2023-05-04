using Findox.Api.Domain.Enumerators;

namespace Findox.Api.Domain.Responses
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRole RoleId { get; set; }
        public int[] Groups { get; set; } = new int[0];
    }
}
