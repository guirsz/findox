using Findox.Api.Domain.Enumerators;

namespace Findox.Api.Domain.Responses
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserRole RoleId { get; set; }
    }
}
