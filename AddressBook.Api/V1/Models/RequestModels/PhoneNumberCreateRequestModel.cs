using AddressBook.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.Api.V1.Models.RequestModels
{
    public class PhoneNumberCreateRequestModel
    {
        [Phone]
        public string Phone { get; set; }

        public PhoneType PhoneType { get; set; }
    }
}
