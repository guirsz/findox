using Findox.Api.Domain;
using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Responses;

namespace Findox.Api.Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DatabaseConfigurations configurations;

        public GroupRepository(DatabaseConfigurations databaseConfigurations)
        {
            configurations = databaseConfigurations;
        }

        public Task<IEnumerable<GroupResponse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GroupEntity> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GroupEntity> GetByName(string groupName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupEntity>> GetManyByIdAsync(int[] groups)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(GroupEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(GroupEntity groupEntity)
        {
            throw new NotImplementedException();
        }
    }
}
