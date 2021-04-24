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
            CreateMap<StateModel, StateViewModel>();
        }
    }
}
