using AutoMapper;
using TechChallange.Api.Controllers.Contact.Dto;
using TechChallange.Api.Controllers.Region.Dto;
using TechChallange.Domain.Contact.Entity;
using TechChallange.Domain.Region.Entity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TechChallange.Api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegionCreateDto, RegionEntity>();
            CreateMap<RegionEntity, RegionResponseDto>();

            CreateMap<ContactCreateDto, ContactEntity>();
            CreateMap<ContactUpdateDto, ContactEntity>();
        }
    }
}
