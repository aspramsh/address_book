using AddressBook.Business.Models.Enums;

namespace AddressBook.Business.Models
{
    public class GeoLocationModel
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public LocationType Type { get; set; }

        public string Name { get; set; }

        public string PostalCode { get; set; }

        public string Region { get; set; }

        public string RegionCode { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public string TimeZone{ get; set; }
    }
}
