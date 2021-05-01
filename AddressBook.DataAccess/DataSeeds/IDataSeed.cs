using AddressBook.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.DataAccess.DataSeeds
{
    public interface IDataSeed
    {
        ICollection<Country> GetCountries();

        Task SeedAllInitialDataAsync(CancellationToken cancellationToken = default);
    }
}
