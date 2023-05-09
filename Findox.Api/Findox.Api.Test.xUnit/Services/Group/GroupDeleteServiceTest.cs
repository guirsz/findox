using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Helpers;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Service.Services.Group;
using Moq;

namespace Findox.Api.Test.xUnit.Services.Group
{
    public class GroupDeleteServiceTest
    {
        private Mock<IGroupRepository> mockGroupRepository = new Mock<IGroupRepository>();

        [Fact]
        public async Task InvalidData()
        {
            mockGroupRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => new GroupEntity() { GroupId = 0 });

            var service = new GroupDeleteService(mockGroupRepository.Object);
            var result = await service.RunAsync(1, 1);

            Assert.Equal(false, result.deleted);
            Assert.Equal(ApplicationMessages.InvalidData, result.message);
        }

        [Fact]
        public async Task RemovedSuccessfullyDeleted()
        {
            mockGroupRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => new GroupEntity() { GroupId = 1, Deleted = true });

            var service = new GroupDeleteService(mockGroupRepository.Object);
            var result = await service.RunAsync(1, 1);

            Assert.Equal(true, result.deleted);
            Assert.Equal(ApplicationMessages.RemovedSuccessfully, result.message);
        }

        [Fact]
        public async Task RemovedSuccessfully()
        {
            mockGroupRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => new GroupEntity() { GroupId = 1, Deleted = false });
            mockGroupRepository.Setup(a => a.UpdateAsync(It.IsAny<GroupEntity>())).ReturnsAsync(() => true);

            var service = new GroupDeleteService(mockGroupRepository.Object);
            var result = await service.RunAsync(1, 1);

            Assert.Equal(true, result.deleted);
            Assert.Equal(ApplicationMessages.RemovedSuccessfully, result.message);
        }
    }
}
