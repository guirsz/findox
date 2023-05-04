using Dapper;
using Findox.Api.Data.Extensions;
using Findox.Api.Domain.Enumerators;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Security;
using Npgsql;

namespace Findox.Api.Data.Repositories
{
    public class InitDatabaseRepository : IInitDatabaseRepository
    {
        private NpgsqlConnection connection;

        public InitDatabaseRepository(IDatabaseAccess database)
        {
            this.connection = database.Connection();
        }

        public async Task InitializeAsync()
        {
            var exists = await connection.QueryAsync<int>("select 1 from users fetch first 1 rows only");

            if (exists.Any() && exists.FirstOrDefault() != 1)
            {
                var salt = Argon2Hash.CreateSalt();
                await CreateAdminUserAsync(connection, salt);
                await CreateManagerUserAsync(connection, salt);
                await CreateRegularUserAsync(connection, salt);
            }
        }

        private static async Task CreateRegularUserAsync(NpgsqlConnection connection, byte[] salt)
        {
            var hashed = Argon2Hash.HashPassword("kimberly.owen", salt);
            await connection.ExecuteScalarAsync("SELECT * FROM fn_users_initialize(@in_role_id, @in_user_name, @in_email, @in_password_hash, @in_password_salt)",
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

        private static async Task CreateManagerUserAsync(NpgsqlConnection connection, byte[] salt)
        {
            var hashed = Argon2Hash.HashPassword("guilherme.souza", salt);
            await connection.ExecuteScalarAsync("SELECT * FROM fn_users_initialize(@in_role_id, @in_user_name, @in_email, @in_password_hash, @in_password_salt)",
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

        private static async Task CreateAdminUserAsync(NpgsqlConnection connection, byte[] salt)
        {
            var hashed = Argon2Hash.HashPassword("brian.bentow", salt);
            await connection.ExecuteScalarAsync("SELECT * FROM fn_users_initialize(@in_role_id, @in_user_name, @in_email, @in_password_hash, @in_password_salt)",
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
