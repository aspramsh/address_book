using AddressBook.Business.Models.Enums;
using Newtonsoft.Json;

namespace AddressBook.Business.Geocoders.Models
{
    public class LocationModel
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public LocationType Type { get; set; }

        public string Name { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        public string Region { get; set; }

        [JsonProperty("region_code")]
        public string RegionCode { get; set; }

        public string Country { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("timezone_module")]
        public TimezoneModel Timezone { get; set; }
    }
}
