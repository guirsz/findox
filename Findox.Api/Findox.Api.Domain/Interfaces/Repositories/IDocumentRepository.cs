using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Domain.Interfaces.Repositories
{
    public interface IDocumentRepository: ICrudRepository<DocumentEntity>
    {
        Task<IEnumerable<DocumentResponse>> GetAllPaginatedAsync(DocumentGetAllPaginatedRequest request, int requestedBy);
        Task<DocumentEntity> GetAsync(Guid id);
        Task<DocumentResponse> GetWithGroupsAsync(Guid id);
        Task GrantAccessToGroupAsync(GrantAccessGroupEntity grantAccessGroupEntity);
        Task GrantAccessToUserAsync(GrantAccessUserEntity grantAccessUserEntity);
        Task RemoveAccessToGroupAsync(Guid id, int groupId);
        Task RemoveAccessToUserAsync(Guid id, int userId);
    }
}
