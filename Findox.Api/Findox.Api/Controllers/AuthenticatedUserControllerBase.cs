using Findox.Api.Domain.Enumerators;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Findox.Api.Controllers
{
    public class AuthenticatedUserControllerBase : ControllerBase
    {
        public AuthenticatedUserControllerBase() { }

        protected int RequestedBy()
        {
            var claimValue = User.Claims?.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            if (claimValue != null)
            {
                if (int.TryParse(claimValue, out var userId))
                    return userId;
            }
            throw new Exception("Missing user information");
        }

        protected UserRole UserRoleId()
        {
            var claimValue = User.Claims?.FirstOrDefault(a => a.Type == ClaimTypes.Role)?.Value;
            if (claimValue != null)
            {
                if (Enum.TryParse(claimValue, out UserRole userRole))
                    return userRole;
            }
            throw new Exception("Missing user information");
        }
    }
}
