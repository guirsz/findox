using Findox.Api.Domain.Interfaces;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public Task<(int userId, string message)> Create(UserRequest request, int requestedBy)
        {
            throw new NotImplementedException();
        }

        public Task<(bool deleted, string message)> Delete(int id, int requestedBy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserResponse>> GetAll(int limit, int offset)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse?> GetById(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<(int userId, string message)> Update(int id, UserRequest request, int requestedBy)
        {
            throw new NotImplementedException();
        }
    }
}
