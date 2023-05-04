using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Helpers;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services.User;

namespace Findox.Api.Service.Services.User
{
    public class UserDeleteService : IUserDeleteService
    {
        private readonly IUserRepository userRepository;

        public UserDeleteService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<(bool deleted, string message)> RunAsync(int id, int requestedBy)
        {
            UserEntity user = await userRepository.GetAsync(id);

            if (user == null || user.UserId == 0)
            {
                return (false, ApplicationMessages.InvalidData);
            }

            if (user.Enabled == false)
            {
                return (true, ApplicationMessages.RemovedSuccessfully);
            }

            user.UpdatedDate = DateTime.Now;
            user.UpdatedBy = requestedBy;
            user.Enabled = false;

            await userRepository.UpdateAsync(user);

            return (true, ApplicationMessages.RemovedSuccessfully);
        }
    }
}
