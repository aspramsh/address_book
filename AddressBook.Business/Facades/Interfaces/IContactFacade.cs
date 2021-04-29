using AddressBook.Business.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Facades.Interfaces
{
    public interface IContactFacade
    {
        Task<ContactModel> GetAsync(
            int id,
            CancellationToken cancellationToken);

        Task<List<ContactModel>> GetListAsync(
            CancellationToken cancellationToken);

        Task<ContactModel> CreateAsync(
            ContactModel model,
            CancellationToken cancellationToken);
    }
}
