using AddressBook.Domain.Models;

namespace AddressBook.DataAccess.Entities
{
    public class Address : BaseAddress<ZipCode, Contact>
    {
    }
}