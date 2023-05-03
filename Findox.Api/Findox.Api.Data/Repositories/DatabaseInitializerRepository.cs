using Dapper;
using Findox.Api.Domain;
using Findox.Api.Domain.Enumerators;
using Findox.Api.Domain.Helpers;
using Findox.Api.Domain.Interfaces;
using Npgsql;

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

                await connection.ExecuteScalarAsync("SELECT * FROM fn_users_create(@user_role_id, @admin_user_name, @admin_user_email, @admin_user_password)",
                    param: new
                    {
                        user_role_id = UserRole.Admin,
                        admin_user_name = "Brian Bentow",
                        admin_user_email = "brian@findox.com",
                        admin_user_password = "brian.bentow".HashPassword(),
                    },
                    commandType: System.Data.CommandType.Text);

                await connection.ExecuteScalarAsync("SELECT * FROM fn_users_create(@user_role_id, @admin_user_name, @admin_user_email, @admin_user_password)",
                    param: new
                    {
                        user_role_id = UserRole.Manager,
                        admin_user_name = "Guilherme Souza",
                        admin_user_email = "guilherme@findox.com",
                        admin_user_password = "guilherme.souza".HashPassword(),
                    },
                    commandType: System.Data.CommandType.Text);

                await connection.ExecuteScalarAsync("SELECT * FROM fn_users_create(@user_role_id, @admin_user_name, @admin_user_email, @admin_user_password)",
                    param: new
                    {
                        user_role_id = UserRole.RegularUser,
                        admin_user_name = "Kimberly Owen",
                        admin_user_email = "kimberly.owen@missionresourcing.com",
                        admin_user_password = "kimberly.owen".HashPassword(),
                    },
                    commandType: System.Data.CommandType.Text);
            }
        }
    }
}
