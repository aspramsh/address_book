using AddressBook.Business.Services;
using AddressBook.Business.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AddressBook.Business
{
    public static class BusinessModule
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IPhoneNumberService, PhoneNumberService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<IZipCodeService, ZipCodeService>();
        }
    }
}
