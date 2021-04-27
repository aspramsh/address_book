using AddressBook.Business.Models;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Facades.Interfaces
{
    public interface IZipCodeFacade
    {
        Task<ZipCodeModel> CreateAsync(
            ZipCodeModel model,
            CancellationToken cancellationToken);
    }
}
