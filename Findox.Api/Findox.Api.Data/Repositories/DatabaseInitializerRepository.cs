using Dapper;
using Findox.Api.Domain;
using Findox.Api.Domain.Enumerators;
using Findox.Api.Domain.Interfaces;
using Npgsql;

namespace Findox.Api.Data.Repositories
{
    public class DatabaseInitializerRepository : IDatabaseInitializerRepository
    {
        private readonly string connectionString;

        public DatabaseInitializerRepository(DatabaseConfigurations databaseConfigurations)
        {
            connectionString = databaseConfigurations.ConnectionString;
        }

        public async Task InitializeDatabase()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                await connection.ExecuteAsync("sp_ensure_initial_data",
                    param: new
                    {
                        user_role_id = (int)UserRoleEnum.Admin,
                        admin_user_name = "admin",
                        admin_user_password = "admin"
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
