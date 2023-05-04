using Dapper;
using Findox.Api.Domain;
using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;
using Npgsql;
using System.Data;

namespace Findox.Api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseConfigurations configurations;

        public UserRepository(DatabaseConfigurations databaseConfigurations)
        {
            configurations = databaseConfigurations;
        }

        public async Task<int> InsertAsync(UserEntity user)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                //var parameters = new DynamicParameters();
                //parameters.Add("in_user_name", user.UserName);
                //parameters.Add("in_email", user.UserName);
                //parameters.Add("in_password_hash", user.UserName);
                //parameters.Add("in_password_salt", user.UserName);
                //parameters.Add("in_role_id", user.UserName);
                //parameters.Add("in_enabled", user.UserName);
                //parameters.Add("in_created_date", user.UserName);
                //parameters.Add("in_created_by", user.UserName);
                //parameters.Add("in_user_name", user.UserName);

                user.UserId = await connection.ExecuteScalarAsync<int>(@"
                    SELECT * FROM fn_users_create
                    (
                        @in_user_name, @in_email, @in_password_hash, @in_password_salt, 
                        @in_role_id, @in_enabled, @in_created_date, @in_created_by, 
                        @in_updated_date, @in_updated_by
                    );",
                    param: new
                    {
                        in_user_name = user.UserName,
                        in_email = user.Email,
                        in_password_hash = user.PasswordHash,
                        in_password_salt = user.PasswordSalt,
                        in_role_id = user.RoleId,
                        in_enabled = user.Enabled,
                        in_created_date = user.CreatedDate,
                        in_created_by = user.CreatedBy,
                        in_updated_date = user.UpdatedDate,
                        in_updated_by = user.UpdatedBy
                    },
                    commandType: CommandType.Text);

                return user.UserId;
            }
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
                    commandType: CommandType.Text);

                return results?.FirstOrDefault(a => a.UserId > 0);
            }
        }

        public Task LinkGroupAsync(UserGroupEntity userGroupEntity)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserResponse>> GetAllPaginatedAsync(UserGetAllPaginatedRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int[]> GetUserGroupsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UnlinkGroupAsync(int userId, IEnumerable<int> groupsToUnlink)
        {
            throw new NotImplementedException();
        }
    }
}
