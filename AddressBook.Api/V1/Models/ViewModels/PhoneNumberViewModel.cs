using AddressBook.Domain.Enums;

namespace AddressBook.Api.V1.Models.ViewModels
{
    public class PhoneNumberViewModel
    {
        public int Id { get; set; }

        public string Phone { get; set; }

        public PhoneType PhoneType { get; set; }
    }
}
