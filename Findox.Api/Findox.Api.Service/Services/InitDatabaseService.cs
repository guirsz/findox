using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services;

namespace Findox.Api.Service.Services
{
    public class InitDatabaseService : IInitDatabaseService
    {
        private readonly IInitDatabaseRepository repository;

        public InitDatabaseService(IInitDatabaseRepository repository)
        {
            this.repository = repository;
        }

        public async Task RunAsync()
        {
            await this.repository.InitializeAsync();
        }
    }
}
