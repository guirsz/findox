using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Helpers;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Requests;
using Findox.Api.Service.Services.Group;
using Moq;

namespace Findox.Api.Test.xUnit.Services.Group
{
    public class GroupUpdateServiceTest
    {
        private Mock<IGroupRepository> mockGroupRepository = new Mock<IGroupRepository>();

        [Fact]
        public async Task InvalidDataCantFindGroup()
        {
            mockGroupRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            var service = new GroupUpdateService(mockGroupRepository.Object);
            var result = await service.RunAsync(new GroupUpdateRequest(), 1);

            Assert.Equal(0, result.groupId);
            Assert.Equal(ApplicationMessages.InvalidData, result.message);
        }

        [Fact]
        public async Task InvalidDataGroupDeleted()
        {
            mockGroupRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => new GroupEntity() { GroupId = 1, Deleted = true });

            var service = new GroupUpdateService(mockGroupRepository.Object);
            var result = await service.RunAsync(new GroupUpdateRequest(), 1);

            Assert.Equal(0, result.groupId);
            Assert.Equal(ApplicationMessages.InvalidData, result.message);
        }

        [Fact]
        public async Task NameInUse()
        {
            mockGroupRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => new GroupEntity() { GroupId = 1, GroupName = "group1" });
            mockGroupRepository.Setup(a => a.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(() => new GroupEntity() { GroupId = 1, GroupName = "newGroupName" });

            var service = new GroupUpdateService(mockGroupRepository.Object);
            var result = await service.RunAsync(new GroupUpdateRequest() { GroupName = "newGroupName" }, 1);

            Assert.Equal(0, result.groupId);
            Assert.Equal(ApplicationMessages.GroupNameInUse, result.message);
        }

        [Fact]
        public async Task CreatedSuccessfully()
        {
            mockGroupRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => new GroupEntity() { GroupId = 1 });
            mockGroupRepository.Setup(a => a.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            mockGroupRepository.Setup(a => a.UpdateAsync(It.IsAny<GroupEntity>())).ReturnsAsync(() => true);

            var service = new GroupUpdateService(mockGroupRepository.Object);
            var result = await service.RunAsync(new GroupUpdateRequest()
            {
                GroupId = 1,
                GroupName = "Example"
            }, 1);

            Assert.Equal(1, result.groupId);
            Assert.Equal(ApplicationMessages.UpdatedSuccessfully, result.message);
        }
    }
}
