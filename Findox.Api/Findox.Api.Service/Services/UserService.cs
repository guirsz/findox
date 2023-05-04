using AutoMapper;
using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Interfaces;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;
using Findox.Api.Domain.Security;

namespace Findox.Api.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<(int userId, string message)> CreateAsync(UserCreateRequest request, int requestedBy)
        {
            var userEntity = await userRepository.FindByEmailAsync(request.Email);

            if (userEntity?.UserId > 0)
            {
                return (0, $"The email is already being used");
            }

            var dateTimeNow = DateTime.Now;

            var user = mapper.Map<UserEntity>(request);
            user.PasswordSalt = Argon2Hash.CreateSalt();
            user.PasswordHash = Argon2Hash.HashPassword(request.Password, user.PasswordSalt);
            user.Enabled = true;
            user.CreatedBy = requestedBy;
            user.CreatedDate = dateTimeNow;
            user.UpdatedBy = requestedBy;
            user.UpdatedDate = dateTimeNow;
            user.UserGroups = request.Groups;

            int userId = await userRepository.CreateAsync(user);

            return (userId, "Created sucessufully");
        }

        public Task<(bool deleted, string message)> DeleteAsync(int id, int requestedBy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserResponse>> GetAllPaginatedAsync(UserGetAllPaginatedRequest query)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse?> GetByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<(int userId, string message)> UpdateAsync(int id, UserUpdateRequest request, int requestedBy)
        {
            throw new NotImplementedException();
        }
    }
}
