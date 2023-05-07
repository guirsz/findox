using Findox.Api.Data.Repositories;
using Findox.Api.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Findox.Api.CrossCutting.DependencyInjection
{
    public static class ConfigureRepository
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddTransient<IInitDatabaseRepository, InitDatabaseRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IDocumentRepository, DocumentRepository>();
        }
    }
}

