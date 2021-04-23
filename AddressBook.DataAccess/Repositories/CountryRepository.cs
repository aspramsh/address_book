using AddressBook.Common.Repositories;
using AddressBook.DataAccess.Entities;
using AddressBook.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace AddressBook.DataAccess.Repositories
{
    public class CountryRepository : EntityFrameworkGenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(
            AddressBookContext dbContext,
            ILoggerFactory loggerFactory)
            : base(dbContext, loggerFactory)
        {
        }
    }
}
