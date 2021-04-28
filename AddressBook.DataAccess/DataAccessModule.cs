using AddressBook.Common.Helpers;
using AddressBook.DataAccess.Repositories;
using AddressBook.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AddressBook.DataAccess
{
    public static class DataAccessModule
    {
        public static void AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AddressBookContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(Constants.ConnectionName)));
        }

        public static void AddRepositories(
            this IServiceCollection services)
        {
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IZipCodeRepository, ZipCodeRepository>();
        }
    }
}
