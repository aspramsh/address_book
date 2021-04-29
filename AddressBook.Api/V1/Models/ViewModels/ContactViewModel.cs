using AddressBook.Api.V1.Models.RequestModels;
using System.Collections.Generic;

namespace AddressBook.Api.V1.Models.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Street { get; set; }

        public int? Building { get; set; }

        public int? Appartment { get; set; }

        public ZipCodeViewModel ZipCode { get; set; }

        public List<PhoneNumberViewModel> PhoneNumbers { get; set; }
    }
}
