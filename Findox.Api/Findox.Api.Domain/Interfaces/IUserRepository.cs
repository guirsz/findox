using Findox.Api.Domain.Entities;

namespace Findox.Api.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity?> FindByEmailAsync(string email);
    }
}
