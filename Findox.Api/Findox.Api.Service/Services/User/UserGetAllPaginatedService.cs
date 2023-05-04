using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services.User;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Service.Services.User
{
    public class UserGetAllPaginatedService : IUserGetAllPaginatedService
    {
        private readonly IUserRepository userRepository;
        
        public UserGetAllPaginatedService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<UserResponse>> RunAsync(UserGetAllPaginatedRequest request)
        {
            return await userRepository.GetAllPaginatedAsync(request);
        }
    }
}
