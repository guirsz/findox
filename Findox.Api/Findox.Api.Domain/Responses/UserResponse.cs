using Findox.Api.Domain.Enumerators;

namespace Findox.Api.Domain.Responses
{
    public class UserResponse
    {
        public UserResponse()
        {
            this.Groups = new List<GroupResponse>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRole RoleId { get; set; }
        public IEnumerable<GroupResponse> Groups { get; set; }
    }
}
