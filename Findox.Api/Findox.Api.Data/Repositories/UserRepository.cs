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

                user.UserId = await connection.ExecuteScalarAsync<int>(@"
                    SELECT fn_users_create(@in_user_name, @in_email, @in_password_hash, @in_password_salt, @in_role_id, @in_deleted, @in_created_date, @in_created_by);",
                    param: new
                    {
                        in_user_name = user.UserName,
                        in_email = user.Email,
                        in_password_hash = user.PasswordHash,
                        in_password_salt = user.PasswordSalt,
                        in_role_id = user.RoleId,
                        in_deleted = user.Deleted,
                        in_created_date = user.CreatedDate,
                        in_created_by = user.CreatedBy
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

        public async Task LinkGroupAsync(UserGroupEntity userGroupEntity)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                await connection.ExecuteScalarAsync(@"
                    SELECT fn_users_link_group(@in_user_id, @in_group_id, @in_grouped_date, @in_grouped_by);",
                    param: new
                    {
                        in_user_id = userGroupEntity.UserId,
                        in_group_id = userGroupEntity.GroupId,
                        in_grouped_date = userGroupEntity.GroupedDate,
                        in_grouped_by = userGroupEntity.GroupedBy,
                    },
                    commandType: CommandType.Text);
            }
        }

        public async Task<IEnumerable<UserResponse>> GetAllPaginatedAsync(UserGetAllPaginatedRequest request)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                var results = await connection.QueryAsync<UserResponse>("SELECT * FROM fn_users_get_all_paginated(@in_limit, @in_offset, @in_filter_text)",
                    param: new
                    {
                        in_limit = request.Limit,
                        in_offset = request.Offset,
                        in_filter_text = $"%{request.FilterText.Trim()}%"
                    },
                    commandType: CommandType.Text);

                return results;
            }
        }

        public async Task<int[]> GetUserGroupsAsync(int id)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                var results = await connection.QueryAsync<int>("SELECT * FROM fn_users_get_user_groups(@in_user_id)",
                    param: new
                    {
                        in_user_id = id
                    },
                    commandType: CommandType.Text);

                return results.ToArray();
            }
        }

        public async Task UnlinkGroupAsync(int userId, IEnumerable<int> groupsToUnlink)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                await connection.ExecuteScalarAsync(@"
                    SELECT fn_users_unlink_group(@in_user_id, @in_group_id);",
                    param: new
                    {
                        in_user_id = userId,
                        in_group_id = groupsToUnlink
                    },
                    commandType: CommandType.Text);
            }
        }

        public async Task<UserEntity> GetAsync(int id)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                var results = await connection.QueryAsync<UserEntity>("SELECT * FROM fn_users_get(@in_user_id)",
                    param: new
                    {
                        in_user_id = id
                    },
                    commandType: CommandType.Text);

                return results?.FirstOrDefault(a => a.UserId == id);
            }
        }

        public async Task<bool> UpdateAsync(UserEntity user)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                user.UserId = await connection.ExecuteScalarAsync<int>(@"
                    SELECT fn_users_update(@in_user_name, @in_email, @in_role_id, @in_deleted, @in_updated_date, @in_updated_by);",
                    param: new
                    {
                        in_user_name = user.UserName,
                        in_email = user.Email,
                        in_role_id = user.RoleId,
                        in_deleted = user.Deleted,
                        in_updated_date = user.UpdatedDate,
                        in_updated_by = user.UpdatedBy
                    },
                    commandType: CommandType.Text);

                return true;
            }
        }
    }
}
