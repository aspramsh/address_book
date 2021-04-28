using AddressBook.Domain.Enums;

namespace AddressBook.Api.V1.Models.RequestModels
{
    public class PhoneNumberCreateRequestModel
    {
        public string Phone { get; set; }

        public PhoneType PhoneType { get; set; }
    }
}
