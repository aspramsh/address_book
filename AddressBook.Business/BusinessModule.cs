using AddressBook.Business.Configuration.Models;
using AddressBook.Business.Facades;
using AddressBook.Business.Facades.Interfaces;
using AddressBook.Business.Geocoders;
using AddressBook.Business.Geocoders.Interfaces;
using AddressBook.Business.Helpers;
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

            services.AddScoped<IStateFacade, StateFacade>();
        }

        public static void RegisterHttpClients(
            this IServiceCollection services,
            HttpRetryPolicySettings retryPolicySettings)
        {
            services.AddHttpClient(nameof(PositionStackHttpClient))
                .AddPolicyHandler(HttpClientHelper.GetRetryPolicy(retryPolicySettings.RetryCount, retryPolicySettings.RetryStartTime));
            services.AddScoped<IPositionStackHttpClient, PositionStackHttpClient>();
        }
    }
}
