using Dapper;
using Findox.Api.Domain;
using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Interfaces;
using Npgsql;

namespace Findox.Api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseConfigurations configurations;

        public UserRepository(DatabaseConfigurations databaseConfigurations)
        {
            configurations = databaseConfigurations;
        }

        public async Task<UserEntity?> FindByEmailAsync(string email)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();
                
                var results = await connection.QueryAsync<UserEntity>("SELECT * FROM fn_users_find_by_email(@user_email)",
                    param: new
                    {
                        user_email = email
                    },
                    commandType: System.Data.CommandType.Text);

                return results?.FirstOrDefault();
            }
        }
    }
}
