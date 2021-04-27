using AddressBook.Business.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Facades.Interfaces
{
    public interface IZipCodeFacade
    {
        Task<List<ZipCodeModel>> GetByAsync(
            int countryId,
            int cityId,
            CancellationToken cancellationToken);

        Task<ZipCodeModel> CreateAsync(
            ZipCodeModel model,
            CancellationToken cancellationToken);
    }
}
