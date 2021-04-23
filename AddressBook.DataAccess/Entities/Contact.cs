using AddressBook.Domain.Models;

namespace AddressBook.DataAccess.Entities
{
    public class Contact : BaseContact<Address, PhoneNumber>
    {
    }
}
