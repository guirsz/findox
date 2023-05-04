using Findox.Api.Data.Extensions;
using Findox.Api.Data.Repositories;
using Findox.Api.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Findox.Api.CrossCutting.DependencyInjection
{
    public static class ConfigureRepository
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseAccess, DatabaseAccess>();
            services.AddTransient<IDatabaseInitializerRepository, DatabaseInitializerRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
