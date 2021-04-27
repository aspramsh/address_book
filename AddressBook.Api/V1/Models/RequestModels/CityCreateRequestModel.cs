namespace AddressBook.Api.V1.Models.RequestModels
{
    public class CityCreateRequestModel
    {
        public string Name { get; set; }

        public int? StateId { get; set; }

        public int CountryId { get; set; }
    }
}
