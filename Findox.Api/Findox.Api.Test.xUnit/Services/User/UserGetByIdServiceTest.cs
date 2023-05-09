using AutoMapper;
using Findox.Api.CrossCutting.Mappings;
using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Helpers;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Service.Services.User;
using Moq;

namespace Findox.Api.Test.xUnit.Services.User
{
    public class UserGetByIdServiceTest
    {
        private Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private readonly IMapper mapper;

        public UserGetByIdServiceTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            this.mapper = mockMapper.CreateMapper();
        }

        [Fact]
        public async Task GetAnotherUserNotAdmin()
        {
            var service = new UserGetByIdService(mockUserRepository.Object, mapper);
            var result = await service.RunAsync(1, 2, Domain.Enumerators.UserRole.RegularUser);

            Assert.Null(result);
        }

        [Fact]
        public async Task UserDoesNotExists()
        {
            mockUserRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            var service = new UserGetByIdService(mockUserRepository.Object, mapper);
            var result = await service.RunAsync(1, 2, Domain.Enumerators.UserRole.Admin);

            Assert.Null(result);
        }

        [Fact]
        public async Task UserDeleted()
        {
            mockUserRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => new UserEntity() { Deleted = true });

            var service = new UserGetByIdService(mockUserRepository.Object, mapper);
            var result = await service.RunAsync(1, 2, Domain.Enumerators.UserRole.Admin);

            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnUser()
        {
            mockUserRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => new UserEntity() { Deleted = true });
            mockUserRepository.Setup(a => a.GetUserGroupsAsync(It.IsAny<int>())).ReturnsAsync(() => new int[] { });

            var service = new UserGetByIdService(mockUserRepository.Object, mapper);
            var result = await service.RunAsync(1, 2, Domain.Enumerators.UserRole.Admin);

            Assert.Null(result);
        }
    }
}
