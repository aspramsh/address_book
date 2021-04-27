namespace AddressBook.Api.V1.Models.RequestModels
{
    public class ZipCodeCreateRequestModel
    {
        public string Code { get; set; }

        public int CityId { get; set; }
    }
}
