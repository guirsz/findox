using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Helpers;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Service.Services.User;
using Moq;

namespace Findox.Api.Test.xUnit.Services.User
{
    public class UserDeleteServiceTest
    {
        private Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();

        [Fact]
        public async Task InvalidData()
        {
            mockUserRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => new UserEntity() { UserId = 0 });

            var service = new UserDeleteService(mockUserRepository.Object);
            var result = await service.RunAsync(1, 1);

            Assert.Equal(false, result.deleted);
            Assert.Equal(ApplicationMessages.InvalidData, result.message);
        }

        [Fact]
        public async Task RemovedSuccessfullyDeleted()
        {
            mockUserRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => new UserEntity() { UserId = 1, Deleted = true });

            var service = new UserDeleteService(mockUserRepository.Object);
            var result = await service.RunAsync(1, 1);

            Assert.Equal(true, result.deleted);
            Assert.Equal(ApplicationMessages.RemovedSuccessfully, result.message);
        }

        [Fact]
        public async Task RemovedSuccessfully()
        {
            mockUserRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => new UserEntity() { UserId = 1, Deleted = false });
            mockUserRepository.Setup(a => a.UpdateAsync(It.IsAny<UserEntity>())).ReturnsAsync(() => true);

            var service = new UserDeleteService(mockUserRepository.Object);
            var result = await service.RunAsync(1, 1);

            Assert.Equal(true, result.deleted);
            Assert.Equal(ApplicationMessages.RemovedSuccessfully, result.message);
        }
    }
}
