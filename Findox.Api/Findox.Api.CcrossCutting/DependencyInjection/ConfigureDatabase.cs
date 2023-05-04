﻿using Findox.Api.Data.Extensions;
using Findox.Api.Domain;
using Findox.Api.Domain.Interfaces.Repositories;
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
        }

        public static async Task InitializeDatabase(this IServiceProvider application)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            var databaseInitializer = application.GetRequiredService<IDatabaseInitializerRepository>();
            await databaseInitializer.InitializeAsync();
        }
    }
}
