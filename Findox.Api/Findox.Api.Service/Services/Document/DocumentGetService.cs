using Findox.Api.Domain.Enumerators;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services.Document;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Service.Services.Document
{
    public class DocumentGetService : IDocumentGetService
    {
        private readonly IDocumentRepository documentRepository;
        private readonly IUserRepository userRepository;

        public DocumentGetService(IDocumentRepository documentRepository, IUserRepository userRepository)
        {
            this.documentRepository = documentRepository;
            this.userRepository = userRepository;
        }

        public async Task<DocumentResponse?> RunAsync(Guid id, int requestedBy, UserRole userRole)
        {
            var document = await documentRepository.GetWithGroupsAsync(id);
            if (document == null || document.DocumentId == Guid.Empty)
            {
                return null;
            }

            if (userRole != UserRole.Admin)
            {
                if (document.GrantedUsers.Contains(requestedBy) == false)
                {
                    var userGroups = await userRepository.GetUserGroupsAsync(requestedBy);
                    var exists = from a in document.GrantedGroups
                                 join b in userGroups on a equals b
                                 select 1;
                    if (exists.Any() == false)
                    {
                        return null;
                    }
                }
            }

            return document;
        }
    }
}
