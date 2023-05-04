using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services.User;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Service.Services.User
{
    public class UserGetByIdService : IUserGetByIdService
    {
        private readonly IUserRepository userRepository;

        public UserGetByIdService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserResponse> RunAsync(int id)
        {
            return await userRepository.GetByIdAsync(id);
        }
    }
}
