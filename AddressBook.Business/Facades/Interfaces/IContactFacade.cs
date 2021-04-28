using AddressBook.Business.Models;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Facades.Interfaces
{
    public interface IContactFacade
    {
        Task<ContactModel> CreateAsync(
            ContactModel model,
            CancellationToken cancellationToken);
    }
}
