using AutoMapper;
using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;

namespace Findox.Api.CrossCutting.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreateRequest, UserEntity>();
            CreateMap<UserEntity, UserResponse>();
            CreateMap<GroupEntity, GroupResponse>();

        }
    }
}
