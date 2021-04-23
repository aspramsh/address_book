using AddressBook.Domain.Models;

namespace AddressBook.DataAccess.Entities
{
    public class City : BaseCity<Country, State, ZipCode>
    {
    }
}
