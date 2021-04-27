using AddressBook.Business.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Facades.Interfaces
{
    public interface ICityFacade
    {
        Task<List<CityModel>> GetByCountryAsync(
            int countryId,
            CancellationToken cancellationToken);

        Task<CityModel> CreateAsync(
            CityModel model,
            CancellationToken cancellationToken);
    }
}
