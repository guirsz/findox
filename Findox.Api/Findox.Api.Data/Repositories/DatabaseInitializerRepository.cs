using Dapper;
using Findox.Api.Domain;
using Findox.Api.Domain.Enumerators;
using Findox.Api.Domain.Interfaces;
using Findox.Api.Domain.Security;
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

                var salt = Argon2Hash.CreateSalt();
                await CreateAdminUser(connection, salt);
                await CreateManagerUser(connection, salt);
                await CreateRegularUser(connection, salt);
            }
        }

        private static async Task CreateRegularUser(NpgsqlConnection connection, byte[] salt)
        {
            var hashed = Argon2Hash.HashPassword("kimberly.owen", salt);
            await connection.ExecuteScalarAsync("SELECT * FROM fn_users_create(@in_role_id, @in_user_name, @in_email, @in_password_hash, @in_password_salt)",
                                param: new
                                {
                                    in_role_id = UserRole.RegularUser,
                                    in_user_name = "Kimberly Owen",
                                    in_email = "kimberly.owen@missionresourcing.com",
                                    in_password_hash = hashed,
                                    in_password_salt = salt
                                },
                                commandType: System.Data.CommandType.Text);
        }

        private static async Task CreateManagerUser(NpgsqlConnection connection, byte[] salt)
        {
            var hashed = Argon2Hash.HashPassword("guilherme.souza", salt);
            await connection.ExecuteScalarAsync("SELECT * FROM fn_users_create(@in_role_id, @in_user_name, @in_email, @in_password_hash, @in_password_salt)",
                                param: new
                                {
                                    in_role_id = UserRole.Manager,
                                    in_user_name = "Guilherme Souza",
                                    in_email = "guilherme@findox.com",
                                    in_password_hash = hashed,
                                    in_password_salt = salt
                                },
                                commandType: System.Data.CommandType.Text);
        }

        private static async Task CreateAdminUser(NpgsqlConnection connection, byte[] salt)
        {
            var hashed = Argon2Hash.HashPassword("brian.bentow", salt);
            await connection.ExecuteScalarAsync("SELECT * FROM fn_users_create(@in_role_id, @in_user_name, @in_email, @in_password_hash, @in_password_salt)",
                                param: new
                                {
                                    in_role_id = UserRole.Admin,
                                    in_user_name = "Brian Bentow",
                                    in_email = "brian@findox.com",
                                    in_password_hash = hashed,
                                    in_password_salt = salt
                                },
                                commandType: System.Data.CommandType.Text);
        }
    }
}
