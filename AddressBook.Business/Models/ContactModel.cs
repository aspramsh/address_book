using AddressBook.Domain.Models;

namespace AddressBook.Business.Models
{
    public class ContactModel : BaseContact<PhoneNumberModel, ZipCodeModel>
    {
    }
}
