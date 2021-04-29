using AddressBook.Domain.Models;

namespace AddressBook.Business.Models
{
    public class ZipCodeModel : BaseZipCode<CountryModel, StateModel, CityModel>
    {
    }
}
