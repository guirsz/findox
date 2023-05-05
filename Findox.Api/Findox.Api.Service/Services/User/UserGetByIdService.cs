using AutoMapper;
using Findox.Api.Domain.Enumerators;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services.User;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Service.Services.User
{
    public class UserGetByIdService : IUserGetByIdService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserGetByIdService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<UserResponse?> RunAsync(int id, int requestedBy, UserRole userRole)
        {
            if (id != requestedBy && userRole != UserRole.Admin)
                return null;

            var userEntity = await userRepository.GetAsync(id);
            
            if (userEntity == null || userEntity.UserId == 0) return null;
            if (userEntity.Deleted) return null;
            
            var result = mapper.Map<UserResponse>(userEntity);
            result.Groups = await userRepository.GetUserGroupsAsync(id);

            return result;
        }
    }
}
