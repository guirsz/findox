using Findox.Api.Domain;
using Npgsql;

namespace Findox.Api.Data.Extensions
{
    public class PostgresDataSource
    {
        private readonly NpgsqlDataSource dataSource;

        public PostgresDataSource(DatabaseConfigurations databaseConfigurations)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(databaseConfigurations.ConnectionString);
            dataSource = dataSourceBuilder.Build();
        }

        public NpgsqlDataSource DataSource
        {
            get { return dataSource; }
        }

    }
}
