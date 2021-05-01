using AddressBook.Business.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Facades.Interfaces
{
    public interface IStateFacade
    {
        Task<List<StateModel>> GetByCountryAsync(
            int countryId,
            string searchValue,
            CancellationToken cancellationToken);

        Task<StateModel> CreateAsync(
            StateModel model,
            CancellationToken cancellationToken);
    }
}
