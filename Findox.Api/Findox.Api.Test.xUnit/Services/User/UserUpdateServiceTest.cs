using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Helpers;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Requests;
using Findox.Api.Service.Services.User;
using Moq;

namespace Findox.Api.Test.xUnit.Services.User
{
    public class UserUpdateServiceTest
    {
        private Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private Mock<IGroupRepository> mockGroupRepository = new Mock<IGroupRepository>();

        [Fact]
        public async Task InvalidDataCantFindUser()
        {
            mockUserRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            var service = new UserUpdateService(mockUserRepository.Object, mockGroupRepository.Object);
            var result = await service.RunAsync(new UserUpdateRequest(), 1);

            Assert.Equal(0, result.userId);
            Assert.Equal(ApplicationMessages.InvalidData, result.message);
        }

        [Fact]
        public async Task InvalidDataUserDeleted()
        {
            mockUserRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => new UserEntity() { UserId = 1, Deleted = true });

            var service = new UserUpdateService(mockUserRepository.Object, mockGroupRepository.Object);
            var result = await service.RunAsync(new UserUpdateRequest(), 1);

            Assert.Equal(0, result.userId);
            Assert.Equal(ApplicationMessages.InvalidData, result.message);
        }

        [Fact]
        public async Task EmailInUse()
        {
            mockUserRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => new UserEntity() { UserId = 1, Email = "email@email.com" });
            mockUserRepository.Setup(a => a.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => new UserEntity() { UserId = 1, Email = "email@email.com" });

            var service = new UserUpdateService(mockUserRepository.Object, mockGroupRepository.Object);
            var result = await service.RunAsync(new UserUpdateRequest() { Email = "newemail@email.com" }, 1);

            Assert.Equal(0, result.userId);
            Assert.Equal(ApplicationMessages.EmailInUse, result.message);
        }

        [Fact]
        public async Task AnyInvalidGroup()
        {
            mockUserRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => new UserEntity() { UserId = 1 });
            mockUserRepository.Setup(a => a.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            mockGroupRepository.Setup(a => a.GetCountAsync(It.IsAny<int[]>())).ReturnsAsync(() => 1);

            var service = new UserUpdateService(mockUserRepository.Object, mockGroupRepository.Object);
            var result = await service.RunAsync(new UserUpdateRequest() { Groups = new int[] { 1, 2 } }, 1);

            Assert.Equal(0, result.userId);
            Assert.Equal(ApplicationMessages.InvalidData, result.message);
        }

        [Fact]
        public async Task CreatedSuccessfully()
        {
            mockUserRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => new UserEntity() { UserId = 1 });
            mockUserRepository.Setup(a => a.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            mockGroupRepository.Setup(a => a.GetCountAsync(It.IsAny<int[]>())).ReturnsAsync(() => 2);
            mockUserRepository.Setup(a => a.UpdateAsync(It.IsAny<UserEntity>())).ReturnsAsync(() => true);
            mockUserRepository.Setup(a => a.LinkGroupAsync(It.IsAny<UserGroupEntity>()));
            mockUserRepository.Setup(a => a.UnlinkGroupAsync(It.IsAny<int>(), It.IsAny<int[]>()));

            var service = new UserUpdateService(mockUserRepository.Object, mockGroupRepository.Object);
            var result = await service.RunAsync(new UserUpdateRequest()
            {
                UserId = 1,
                UserName = "Example",
                Email = "example@example.com",
                RoleId = default,
                Groups = new int[] { 1, 2 }
            }, 1);

            Assert.Equal(1, result.userId);
            Assert.Equal(ApplicationMessages.UpdatedSuccessfully, result.message);
        }
    }
}
