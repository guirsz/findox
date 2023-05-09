using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Enumerators;
using Findox.Api.Domain.Helpers;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services.Document;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Security;

namespace Findox.Api.Service.Services.Document
{
    public class DocumentUpdateService : IDocumentUpdateService
    {
        private readonly IDocumentGetService documentGetService;
        private readonly IDocumentRepository documentRepository;
        private readonly UploadConfigurations uploadConfigurations;

        public DocumentUpdateService(IDocumentGetService documentGetService, IDocumentRepository documentRepository, UploadConfigurations uploadConfigurations)
        {
            this.documentGetService = documentGetService;
            this.documentRepository = documentRepository;
            this.uploadConfigurations = uploadConfigurations;
        }

        public async Task<(Guid documentId, string message)> RunAsync(DocumentRequest request, int requestedBy, UserRole userRole)
        {
            if (string.IsNullOrEmpty(request.FileName))
                return (Guid.Empty, ApplicationMessages.InvalidData);

            if (uploadConfigurations.FileTypeAllowed(request.FileName) == false)
                return (Guid.Empty, ApplicationMessages.InvalidFileName);

            var document = await documentGetService.RunAsync(request.DocumentId, requestedBy, userRole);

            if (document == null || document.DocumentId == Guid.Empty)
                return (Guid.Empty, ApplicationMessages.InvalidData);

            if (document.FileName != request.FileName)
            {
                await UpdateDocumentDataAsync(request.DocumentId, request, requestedBy);
            }

            await GrantUsersAsync(request.DocumentId, request, requestedBy, document);

            await GrantGroupsAsync(request.DocumentId, request, requestedBy, document);

            return (request.DocumentId, ApplicationMessages.UpdatedSuccessfully);
        }

        private async Task UpdateDocumentDataAsync(Guid id, DocumentRequest request, int requestedBy)
        {
            var documentEntity = await documentRepository.GetAsync(id);
            documentEntity.FileName = request.FileName;
            documentEntity.UpdatedDate = DateTime.Now;
            documentEntity.UpdatedBy = requestedBy;
            await documentRepository.UpdateAsync(documentEntity);
        }

        private async Task GrantGroupsAsync(Guid id, DocumentRequest request, int requestedBy, Domain.Responses.DocumentResponse document)
        {
            var groupsToAdd = request.GrantedGroups.Where(a => document.GrantedGroups.Any(b => b == a) == false);
            foreach (var groupIdToAdd in groupsToAdd)
            {
                await documentRepository.GrantAccessToGroupAsync(new GrantAccessGroupEntity()
                {
                    GroupId = groupIdToAdd,
                    DocumentId = id,
                    GrantedDate = DateTime.Now,
                    GrantedBy = requestedBy
                });
            }

            var groupsToRemove = document.GrantedGroups.Where(a => request.GrantedGroups.Any(b => b == a) == false);
            foreach (var groupIdToRemove in groupsToRemove)
            {
                await documentRepository.RemoveAccessToGroupAsync(id, groupIdToRemove);
            }
        }

        private async Task GrantUsersAsync(Guid id, DocumentRequest request, int requestedBy, Domain.Responses.DocumentResponse document)
        {
            var usersToAdd = request.GrantedUsers.Where(a => document.GrantedUsers.Any(b => b == a) == false);
            foreach (var userIdToAdd in usersToAdd)
            {
                await documentRepository.GrantAccessToUserAsync(new GrantAccessUserEntity()
                {
                    UserId = userIdToAdd,
                    DocumentId = id,
                    GrantedDate = DateTime.Now,
                    GrantedBy = requestedBy
                });
            }

            var usersToRemove = document.GrantedUsers.Where(a => request.GrantedUsers.Any(b => b == a) == false);
            foreach (var userIdToRemove in usersToRemove)
            {
                await documentRepository.RemoveAccessToUserAsync(id, userIdToRemove);
            }
        }
    }
}
