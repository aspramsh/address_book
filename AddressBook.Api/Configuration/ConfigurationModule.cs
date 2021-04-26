using AddressBook.Business.Configuration.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AddressBook.Api.Configuration
{
    public static class ConfigurationModule
    {
        /// <summary>
        /// Adds config models.
        /// </summary>
        /// <param name="services">Services collection.</param>
        /// <param name="configuration">Config.</param>
        public static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GeoLocationSettings>(configuration.GetSection(nameof(GeoLocationSettings)));
        }
    }
}
