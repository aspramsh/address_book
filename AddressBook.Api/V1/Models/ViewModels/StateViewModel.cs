namespace AddressBook.Api.V1.Models.ViewModels
{
    public class StateViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public CountryViewModel Country { get; set; }
    }
}
