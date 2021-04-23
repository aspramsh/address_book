using AddressBook.Domain.Models;

namespace AddressBook.DataAccess.Entities
{
    public class Country : BaseCountry<State, City, ZipCode>
    {
    }
}
