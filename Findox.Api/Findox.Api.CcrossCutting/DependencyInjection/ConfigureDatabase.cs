using Dapper;
using Findox.Api.Data.Extensions;
using Findox.Api.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Findox.Api.CrossCutting.DependencyInjection
{
    public static class ConfigureDatabase
    {
        public static void ConfigureDatabaseConfigurations(this IServiceCollection services, ConfigurationManager configuration)
        {
            var dbConfigurations = new DatabaseConfigurations();
            new ConfigureFromConfigurationOptions<DatabaseConfigurations>(configuration.GetSection("DatabaseConfigurations")).Configure(dbConfigurations);
            services.AddSingleton(dbConfigurations);

            var dataSource = new PostgresDataSource(dbConfigurations);
            services.AddSingleton(dataSource);

            services.AddScoped<IDatabaseAccess, DatabaseAccess>();

            ConfigureDapper();
        }

        private static void ConfigureDapper()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            SqlMapper.AddTypeHandler(new GenericArrayHandler<int>());
        }
    }
}
