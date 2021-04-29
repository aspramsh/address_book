using AddressBook.Business.Models;
using AddressBook.Common.Services;
using AddressBook.DataAccess.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Services.Interfaces
{
    public interface IContactService : IGenericService<ContactModel, Contact>
    {
        Task<ContactModel> CreateSingleAsync(
            ContactModel model,
            CancellationToken cancellationToken);
    }
}
