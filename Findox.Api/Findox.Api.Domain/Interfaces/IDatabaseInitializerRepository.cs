namespace Findox.Api.Domain.Interfaces
{
    public interface IDatabaseInitializerRepository
    {
        public Task InitializeAsync();
    }
}
