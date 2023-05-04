using Findox.Api.Domain;
using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Interfaces.Repositories;

namespace Findox.Api.Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DatabaseConfigurations configurations;

        public GroupRepository(DatabaseConfigurations databaseConfigurations)
        {
            configurations = databaseConfigurations;
        }

        public Task<GroupEntity> SelectByIdAsync(int groupId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupEntity>> SelectManyByIdAsync(int[] groups)
        {
            throw new NotImplementedException();
        }
    }
}
