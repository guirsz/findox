using Findox.Api.Domain.Entities;

namespace Findox.Api.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<int> CreateAsync(UserEntity user);
        Task<UserEntity?> FindByEmailAsync(string email);
    }
}
