using Dapper;
using Findox.Api.Domain;
using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;
using Npgsql;
using System.Data;
using static Dapper.SqlMapper;

namespace Findox.Api.Data.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DatabaseConfigurations configurations;

        public DocumentRepository(DatabaseConfigurations databaseConfigurations)
        {
            configurations = databaseConfigurations;
        }

        public async Task<IEnumerable<DocumentResponse>> GetAllPaginatedAsync(DocumentGetAllPaginatedRequest request, int requestedBy)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                var results = await connection.QueryAsync<DocumentResponse>("SELECT * FROM fn_documents_get_all_paginated(@in_limit, @in_offset, @in_filter_text, @in_user_id)",
                    param: new
                    {
                        in_limit = request.Limit,
                        in_offset = request.Offset,
                        in_filter_text = $"%{request.FilterFileName.Trim()}%",
                        in_user_id = requestedBy
                    },
                    commandType: CommandType.Text);

                return results;
            }
        }

        public Task<DocumentEntity> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<DocumentResponse> GetWithGroupsAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                var results = await connection.QueryAsync<DocumentResponse>("SELECT * FROM fn_documents_get_with_groups(@in_document_id)",
                    param: new
                    {
                        in_document_id = id
                    },
                    commandType: CommandType.Text);

                return results?.FirstOrDefault(a => a.DocumentId == id);
            }
        }

        public async Task GrantAccessToGroupAsync(GrantAccessGroupEntity entity)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                await connection.ExecuteScalarAsync(@"
                    SELECT fb_documents_grant_access_to_group(@in_document_id, @in_group_id, @in_granted_date, @in_granted_by);",
                    param: new
                    {
                        in_document_id = entity.DocumentId,
                        in_group_id = entity.GroupId,
                        in_granted_date = entity.GrantedDate,
                        in_granted_by = entity.GrantedBy
                    },
                    commandType: CommandType.Text);
            }
        }

        public async Task GrantAccessToUserAsync(GrantAccessUserEntity entity)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                await connection.ExecuteScalarAsync(@"
                    SELECT fb_documents_grant_access_to_group(@in_document_id, @in_user_id, @in_granted_date, @in_granted_by);",
                    param: new
                    {
                        in_document_id = entity.DocumentId,
                        in_user_id = entity.UserId,
                        in_granted_date = entity.GrantedDate,
                        in_granted_by = entity.GrantedBy
                    },
                    commandType: CommandType.Text);
            }
        }

        public async Task<int> InsertAsync(DocumentEntity entity)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                await connection.ExecuteScalarAsync<Guid>(@"
                    SELECT fn_documents_create(@in_document_id, @in_file_name, @in_file_length, @in_deleted, @in_created_date, @in_created_by);",
                    param: new
                    {
                        in_document_id = entity.DocumentId,
                        in_file_name = entity.FileName,
                        in_file_length = entity.FileLength,
                        in_deleted = entity.Deleted,
                        in_created_date = entity.CreatedDate,
                        in_created_by = entity.CreatedBy
                    },
                    commandType: CommandType.Text);

                return 1;
            }
        }

        public async Task RemoveAccessToGroupAsync(Guid id, int groupId)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                await connection.ExecuteScalarAsync(@"
                    SELECT fb_documents_remove_access_to_group(@in_document_id, @in_group_id);",
                    param: new
                    {
                        in_document_id = id,
                        in_group_id = groupId
                    },
                    commandType: CommandType.Text);
            }
        }

        public async Task RemoveAccessToUserAsync(Guid id, int userId)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                await connection.ExecuteScalarAsync(@"
                    SELECT fb_documents_remove_access_to_user(@in_document_id, @in_user_id);",
                    param: new
                    {
                        in_document_id = id,
                        in_user_id = userId
                    },
                    commandType: CommandType.Text);
            }
        }

        public async Task<bool> UpdateAsync(DocumentEntity entity)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                await connection.ExecuteScalarAsync<int>(@"
                    SELECT fn_documents_update(@in_document_id, @in_file_name, @in_deleted, @in_updated_date, @in_updated_by);",
                    param: new
                    {
                        in_document_id = entity.DocumentId,
                        in_file_name = entity.FileName,
                        in_deleted = entity.Deleted,
                        in_updated_date = entity.UpdatedDate,
                        in_updated_by = entity.UpdatedBy
                    },
                    commandType: CommandType.Text);

                return true;
            }
        }

        async Task<DocumentEntity> IDocumentRepository.GetAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(configurations.ConnectionString))
            {
                await connection.OpenAsync();

                var results = await connection.QueryAsync<DocumentEntity>("SELECT * FROM fn_users_get(@fn_documents_get)",
                    param: new
                    {
                        in_document_id = id
                    },
                    commandType: CommandType.Text);

                return results?.FirstOrDefault(a => a.DocumentId == id);
            }
        }
    }
}
