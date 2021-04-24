using AddressBook.Business.Models;
using AddressBook.Common.Services;
using AddressBook.DataAccess.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Services.Interfaces
{
    public interface IStateService : IGenericService<StateModel, State>
    {
        Task<StateModel> CreateSingleAsync(
            StateModel model,
            CancellationToken cancellationToken);
    }
}
