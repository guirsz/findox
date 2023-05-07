using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Data.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        public DocumentRepository()
        {

        }

        public Task<IEnumerable<DocumentResponse>> GetAllPaginatedAsync(DocumentGetAllPaginatedRequest request, int requestedBy)
        {
            throw new NotImplementedException();
        }

        public async Task<DocumentResponse> GetAsync(Guid id)
        {
            return await Task.FromResult(new DocumentResponse()
            {
                DocumentId = id,
                FileName = "File.pdf",
            });
        }

        public Task<DocumentEntity> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DocumentResponse> GetWithGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task GrantAccessToGroupAsync(GrantAccessGroupEntity grantAccessGroupEntity)
        {
            throw new NotImplementedException();
        }

        public Task GrantAccessToUserAsync(GrantAccessUserEntity grantAccessUserEntity)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(DocumentEntity entity)
        {
            return Task.FromResult(0);
        }

        public Task RemoveAccessToUserAsync(Guid id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(DocumentEntity groupEntity)
        {
            throw new NotImplementedException();
        }

        Task<DocumentEntity> IDocumentRepository.GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
