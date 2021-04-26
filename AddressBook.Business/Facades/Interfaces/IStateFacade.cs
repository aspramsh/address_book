using AddressBook.Business.Models;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Facades.Interfaces
{
    public interface IStateFacade
    {
        Task<StateModel> CreateAsync(
            StateModel model,
            CancellationToken cancellationToken);
    }
}
