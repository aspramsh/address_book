using AddressBook.Business.Models;
using AddressBook.Common.Services;
using AddressBook.DataAccess.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Services.Interfaces
{
    public interface ICityService : IGenericService<CityModel, City>
    {
        Task<CityModel> CreateSingleAsync(
            CityModel model,
            CancellationToken cancellationToken);
    }
}
