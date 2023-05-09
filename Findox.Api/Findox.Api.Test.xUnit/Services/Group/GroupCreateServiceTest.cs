using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Helpers;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Requests;
using Findox.Api.Service.Services.Group;
using Moq;

namespace Findox.Api.Test.xUnit.Services.Group
{
    public class GroupCreateServiceTest
    {
        private Mock<IGroupRepository> mockGroupRepository = new Mock<IGroupRepository>();

        [Fact]
        public async Task GroupAlreadyExists()
        {
            mockGroupRepository.Setup(a => a.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(() => new GroupEntity() { Deleted = false });

            var service = new GroupCreateService(mockGroupRepository.Object);
            var result = await service.RunAsync(new GroupCreateRequest(), 1);

            Assert.Equal(0, result.groupId);
            Assert.Equal(ApplicationMessages.GroupAlreadyExists, result.message);
        }

        [Fact]
        public async Task GroupDeleted()
        {
            mockGroupRepository.Setup(a => a.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(() => new GroupEntity() { GroupId = 1, Deleted = true });
            mockGroupRepository.Setup(a => a.UpdateAsync(It.IsAny<GroupEntity>()));

            var service = new GroupCreateService(mockGroupRepository.Object);
            var result = await service.RunAsync(new GroupCreateRequest(), 1);

            Assert.Equal(1, result.groupId);
            Assert.Equal(ApplicationMessages.CreatedSuccessfully, result.message);
        }

        [Fact]
        public async Task CreatedSuccessfully()
        {
            mockGroupRepository.Setup(a => a.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            mockGroupRepository.Setup(a => a.InsertAsync(It.IsAny<GroupEntity>())).ReturnsAsync(() => 2);

            var service = new GroupCreateService(mockGroupRepository.Object);
            var result = await service.RunAsync(new GroupCreateRequest(), 1);

            Assert.Equal(2, result.groupId);
            Assert.Equal(ApplicationMessages.CreatedSuccessfully, result.message);
        }
    }
}
