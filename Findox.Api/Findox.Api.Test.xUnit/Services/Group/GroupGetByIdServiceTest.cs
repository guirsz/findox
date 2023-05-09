using AutoMapper;
using Findox.Api.CrossCutting.Mappings;
using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Service.Services.Group;
using Moq;

namespace Findox.Api.Test.xUnit.Services.Group
{
    public class GroupGetByIdServiceTest
    {
        private Mock<IGroupRepository> mockGroupRepository = new Mock<IGroupRepository>();
        private readonly IMapper mapper;

        public GroupGetByIdServiceTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            this.mapper = mockMapper.CreateMapper();
        }


        [Fact]
        public async Task GroupDoesNotExists()
        {
            mockGroupRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            var service = new GroupGetByIdService(mockGroupRepository.Object, mapper);
            var result = await service.RunAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GroupDeleted()
        {
            mockGroupRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => new GroupEntity() { Deleted = true });

            var service = new GroupGetByIdService(mockGroupRepository.Object, mapper);
            var result = await service.RunAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnGroup()
        {
            mockGroupRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(() => new GroupEntity() { GroupId = 1, Deleted = false });

            var service = new GroupGetByIdService(mockGroupRepository.Object, mapper);
            var result = await service.RunAsync(1);

            Assert.NotNull(result);
        }
    }
}
