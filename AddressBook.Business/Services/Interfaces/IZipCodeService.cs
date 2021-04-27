using AddressBook.Business.Models;
using AddressBook.Common.Services;
using AddressBook.DataAccess.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Services.Interfaces
{
    public interface IZipCodeService : IGenericService<ZipCodeModel, ZipCode>
    {
        Task<ZipCodeModel> CreateSingleAsync(
            ZipCodeModel model,
            CancellationToken cancellationToken);
    }
}
