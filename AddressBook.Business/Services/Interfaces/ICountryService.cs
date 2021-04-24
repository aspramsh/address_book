using AddressBook.Business.Models;
using AddressBook.Common.Services;
using AddressBook.DataAccess.Entities;

namespace AddressBook.Business.Services.Interfaces
{
    public interface ICountryService : IGenericService<CountryModel, Country>
    {
    }
}
