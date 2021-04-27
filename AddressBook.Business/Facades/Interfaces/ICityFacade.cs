using AddressBook.Business.Models;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Facades.Interfaces
{
    public interface ICityFacade
    {
        Task<CityModel> CreateAsync(
            CityModel model,
            CancellationToken cancellationToken);
    }
}
