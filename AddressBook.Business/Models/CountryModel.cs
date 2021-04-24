using AddressBook.Domain.Models;

namespace AddressBook.Business.Models
{
    public class CountryModel : BaseCountry<StateModel, CityModel, ZipCodeModel>
    {
    }
}
