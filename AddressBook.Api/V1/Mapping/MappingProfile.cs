using AddressBook.Api.V1.Models.RequestModels;
using AddressBook.Api.V1.Models.ViewModels;
using AddressBook.Business.Models;
using AutoMapper;

namespace AddressBook.Api.V1.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StateCreateRequestModel, StateModel>();
            CreateMap<CityCreateRequestModel, CityModel>();
            CreateMap<ZipCodeCreateRequestModel, ZipCodeModel>();
            CreateMap<PhoneNumberCreateRequestModel, PhoneNumberModel>();
            CreateMap<ContactCreateRequestModel, ContactModel>();
            CreateMap<ContactUpdateRequestModel, ContactModel>();

            CreateMap<CountryModel, CountryViewModel>();
            CreateMap<StateModel, StateViewModel>();
            CreateMap<CityModel, CityViewModel>();
            CreateMap<ZipCodeModel, ZipCodeViewModel>();
            CreateMap<PhoneNumberModel, PhoneNumberViewModel>();
            CreateMap<ContactModel, ContactViewModel>();
        }
    }
}
