using AddressBook.Domain.Models;

namespace AddressBook.Business.Models
{
    public class CityModel : BaseCity<CountryModel, StateModel, ZipCodeModel>
    {
    }
}