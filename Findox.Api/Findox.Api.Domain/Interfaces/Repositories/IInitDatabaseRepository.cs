namespace Findox.Api.Domain.Interfaces.Repositories
{
    public interface IInitDatabaseRepository
    {
        public Task InitializeAsync();
    }
}
