namespace AddressBook.Api.V1.Models.ViewModels
{
    public class ZipCodeViewModel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public CityViewModel City { get; set; }
    }
}
