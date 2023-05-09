using AutoMapper;
using Findox.Api.CrossCutting.Mappings;
using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Helpers;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Requests;
using Findox.Api.Service.Services.User;
using Moq;

namespace Findox.Api.Test.xUnit.Services.User
{
    public class UserCreateServiceTest
    {
        private Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private Mock<IGroupRepository> mockGroupRepository = new Mock<IGroupRepository>();
        private readonly IMapper mapper;

        public UserCreateServiceTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            this.mapper = mockMapper.CreateMapper();
        }

        [Fact]
        public async Task EmailUsed()
        {
            mockUserRepository.Setup(a => a.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => new UserEntity() { UserId = 1, Deleted = false });

            var service = new UserCreateService(mockUserRepository.Object, mockGroupRepository.Object, mapper);
            var result = await service.RunAsync(new UserCreateRequest() { Email = "email@email.com" }, 1);

            Assert.Equal(0, result.userId);
            Assert.Equal(ApplicationMessages.EmailUsed, result.message);
        }

        [Fact]
        public async Task UserDeleted()
        {
            mockUserRepository.Setup(a => a.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => new UserEntity() { UserId = 1, Deleted = true });

            var service = new UserCreateService(mockUserRepository.Object, mockGroupRepository.Object, mapper);
            var result = await service.RunAsync(new UserCreateRequest() { Email = "email@email.com" }, 1);

            Assert.Equal(0, result.userId);
            Assert.Equal(ApplicationMessages.UserDeleted, result.message);
        }

        [Fact]
        public async Task AnyInvalidGroup()
        {
            mockUserRepository.Setup(a => a.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            mockGroupRepository.Setup(a => a.GetCountAsync(It.IsAny<int[]>())).ReturnsAsync(() => 1);

            var service = new UserCreateService(mockUserRepository.Object, mockGroupRepository.Object, mapper);
            var result = await service.RunAsync(new UserCreateRequest() { Groups = new int[] { 1, 2 } }, 1);

            Assert.Equal(0, result.userId);
            Assert.Equal(ApplicationMessages.InvalidData, result.message);
        }


        [Fact]
        public async Task CreatedSuccessfully()
        {
            mockUserRepository.Setup(a => a.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            mockGroupRepository.Setup(a => a.GetCountAsync(It.IsAny<int[]>())).ReturnsAsync(() => 2);
            mockUserRepository.Setup(a => a.InsertAsync(It.IsAny<UserEntity>())).ReturnsAsync(() => 1);
            mockUserRepository.Setup(a => a.LinkGroupAsync(It.IsAny<UserGroupEntity>()));

            var service = new UserCreateService(mockUserRepository.Object, mockGroupRepository.Object, mapper);
            var result = await service.RunAsync(new UserCreateRequest()
            {
                UserName = "Example",
                Email = "example@example.com",
                Password = "example",
                RoleId = default,
                Groups = new int[] { 1, 2 }
            }, 1);

            Assert.Equal(1, result.userId);
            Assert.Equal(ApplicationMessages.CreatedSuccessfully, result.message);
        }
    }
}
