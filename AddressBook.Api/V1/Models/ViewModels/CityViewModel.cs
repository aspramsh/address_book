namespace AddressBook.Api.V1.Models.ViewModels
{
    public class CityViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CountryViewModel Country { get; set; }

        public StateViewModel State { get; set; }
    }
}
