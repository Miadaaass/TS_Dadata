using AutoMapper;
using TS_Dadata.Models;
namespace TS_Dadata.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddressRequest, AddressResponse>();
        }
    }
}

