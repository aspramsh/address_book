using AddressBook.Common.Repositories;
using AddressBook.DataAccess.Entities;

namespace AddressBook.DataAccess.Repositories.Interfaces
{
    public interface ICityRepository : IEntityFrameworkGenericRepository<City>
    {
    }
}
