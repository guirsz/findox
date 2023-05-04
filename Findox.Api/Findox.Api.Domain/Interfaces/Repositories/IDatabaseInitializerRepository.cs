namespace Findox.Api.Domain.Interfaces.Repositories
{
    public interface IDatabaseInitializerRepository
    {
        public Task InitializeAsync();
    }
}
