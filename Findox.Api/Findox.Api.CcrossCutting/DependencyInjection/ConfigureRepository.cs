using Findox.Api.Data.Repositories;
using Findox.Api.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Findox.Api.CrossCutting.DependencyInjection
{
    public static class ConfigureRepository
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddTransient<IDatabaseInitializerRepository, DatabaseInitializerRepository>();
        }
    }
}
