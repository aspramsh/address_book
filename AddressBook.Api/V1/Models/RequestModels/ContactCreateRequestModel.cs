using System.Collections.Generic;

namespace AddressBook.Api.V1.Models.RequestModels
{
    public class ContactCreateRequestModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Street { get; set; }

        public int? Building { get; set; }

        public int? Appartment { get; set; }

        public int? ZipCodeId { get; set; }

        public List<PhoneNumberCreateRequestModel> PhoneNumbers { get; set; }
    }
}
