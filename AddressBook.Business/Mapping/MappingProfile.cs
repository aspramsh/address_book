using AddressBook.Business.Geocoders.Models;
using AddressBook.Business.Models;
using AddressBook.DataAccess.Entities;
using AutoMapper;

namespace AddressBook.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddressModel, Address>()
                .ReverseMap();

            CreateMap<CityModel, City>()
                .ReverseMap();

            CreateMap<ContactModel, Contact>()
                .ReverseMap();

            CreateMap<CountryModel, Country>()
                .ReverseMap();

            CreateMap<PhoneNumberModel, PhoneNumber>()
                .ReverseMap();

            CreateMap<StateModel, State>()
                .ReverseMap();

            CreateMap<ZipCodeModel, ZipCode>()
                .ReverseMap();

            CreateMap<LocationModel, GeoLocationModel>()
                .ForMember(dest => dest.TimeZone,
                opt => opt.MapFrom(src => src.Timezone.name));
        }
    }
}
