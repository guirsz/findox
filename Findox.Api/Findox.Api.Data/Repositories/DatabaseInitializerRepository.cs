using Dapper;
using Findox.Api.Domain;
using Findox.Api.Domain.Enumerators;
using Findox.Api.Domain.Interfaces;
using Npgsql;
using Findox.Api.Domain.Helpers;

namespace Findox.Api.Data.Repositories
{
    public class DatabaseInitializerRepository : IDatabaseInitializerRepository
    {
        private readonly DatabaseConfigurations configurations;

        public DatabaseInitializerRepository(DatabaseConfigurations databaseConfigurations)
        {
            configurations = databaseConfigurations;
        }

        public async Task Initialize()
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                await connection.ExecuteAsync("sp_ensure_initial_data",
                    param: new
                    {
                        user_role_id = UserRoleEnum.Admin,
                        admin_user_name = "guirsz",
                        admin_user_email = "guirsz@gmail.com",
                        admin_user_password = "guirsz".HashPassword(),
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
