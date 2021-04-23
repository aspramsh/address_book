using AddressBook.Common.Repositories;
using AddressBook.DataAccess.Entities;
using AddressBook.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace AddressBook.DataAccess.Repositories
{
    public class CityRepository : EntityFrameworkGenericRepository<City>, ICityRepository
    {
        public CityRepository(
            AddressBookContext dbContext,
            ILoggerFactory loggerFactory)
            : base(dbContext, loggerFactory)
        {
        }
    }
}
