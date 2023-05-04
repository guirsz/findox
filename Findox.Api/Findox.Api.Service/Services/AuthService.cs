using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Security;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Findox.Api.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository repository;
        private readonly SigningConfigurations signingConfigurations;
        private readonly TokenConfigurations tokenConfigurations;

        public AuthService(IUserRepository repository,
                           SigningConfigurations signingConfigurations,
                           TokenConfigurations tokenConfigurations)
        {
            this.repository = repository;
            this.signingConfigurations = signingConfigurations;
            this.tokenConfigurations = tokenConfigurations;
        }

        public async Task<object> RunAsync(AuthRequest request)
        {
            if (request != null && !string.IsNullOrWhiteSpace(request.Email) && !string.IsNullOrWhiteSpace(request.Password))
            {
                var baseUser = await repository.FindByEmailAsync(request.Email);

                if (baseUser != null && Argon2Hash.VerifyHash(request.Password, baseUser.PasswordSalt, baseUser.PasswordHash))
                {
                    DateTime createDate = DateTime.Now;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(tokenConfigurations.Seconds);
                    return SuccessObject(baseUser, createDate, expirationDate);
                }
            }
            return new
            {
                authenticated = false,
                message = "Authentication Failure."
            };
        }

        private object SuccessObject(UserEntity user, DateTime createDate, DateTime expirationDate)
        {
            var identity = new ClaimsIdentity(
                new GenericIdentity(user.Email),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // jti token ID
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString())
                }
            );

            var handler = new JwtSecurityTokenHandler();
            string token = CreateToken(identity, createDate, expirationDate, handler);

            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                accessToken = token,
                userEmail = user.Email,
                userName = user.UserName,
                message = "User logged in successfully."
            };
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredencials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }
    }
}
