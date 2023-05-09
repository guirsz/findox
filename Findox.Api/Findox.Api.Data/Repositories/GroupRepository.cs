using Dapper;
using Findox.Api.Domain;
using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Responses;
using Npgsql;
using System.Data;
using static Dapper.SqlMapper;

namespace Findox.Api.Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DatabaseConfigurations configurations;

        public GroupRepository(DatabaseConfigurations databaseConfigurations)
        {
            configurations = databaseConfigurations;
        }

        public async Task<GroupResponse[]> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                var results = await connection.QueryAsync<GroupResponse>("SELECT * FROM fn_groups_get_all()", commandType: CommandType.Text);

                return results.ToArray();
            }
        }

        public async Task<GroupEntity> GetAsync(int id)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                var results = await connection.QueryAsync<GroupEntity>("SELECT * FROM fn_groups_get(@in_group_id)",
                    param: new
                    {
                        in_group_id = id
                    },
                    commandType: CommandType.Text);

                return results?.FirstOrDefault(a => a.GroupId == id);
            }
        }

        public async Task<GroupEntity> GetByNameAsync(string groupName)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                var results = await connection.QueryAsync<GroupEntity>("SELECT * FROM fn_groups_get_by_name(@in_group_name)",
                    param: new
                    {
                        in_group_name = groupName
                    },
                    commandType: CommandType.Text);

                return results?.FirstOrDefault(a => a.GroupName == groupName);
            }
        }

        public async Task<int> GetCountAsync(int[] groups)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                var results = await connection.QueryAsync<int>("SELECT fn_groups_get_count(@in_group_id)",
                    param: new
                    {
                        in_group_id = groups
                    },
                    commandType: CommandType.Text);

                return results.FirstOrDefault();
            }
        }

        public async Task<int> InsertAsync(GroupEntity entity)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                entity.GroupId = await connection.ExecuteScalarAsync<int>(@"
                    SELECT fn_groups_create (@in_group_name, @in_deleted, @in_created_date, @in_created_by);",
                    param: new
                    {
                        in_group_name = entity.GroupName,
                        in_deleted = entity.Deleted,
                        in_created_date = entity.CreatedDate,
                        in_created_by = entity.CreatedBy
                    },
                    commandType: CommandType.Text);

                return entity.GroupId;
            }
        }

        public async Task<bool> UpdateAsync(GroupEntity entity)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                await connection.ExecuteScalarAsync(@"
                    SELECT fn_groups_update (@in_group_id, @in_group_name, @in_deleted, @in_updated_date, @in_updated_by);",
                    param: new
                    {
                        in_group_id = entity.GroupId,
                        in_group_name = entity.GroupName,
                        in_deleted = entity.Deleted,
                        in_updated_date = entity.UpdatedDate,
                        in_updated_by = entity.UpdatedBy
                    },
                    commandType: CommandType.Text);

                return true;
            }
        }
    }
}
