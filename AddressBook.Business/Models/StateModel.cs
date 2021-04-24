using AddressBook.Domain.Models;

namespace AddressBook.Business.Models
{
    public class StateModel : BaseState<CountryModel, CityModel, ZipCodeModel>
    {
    }
}
