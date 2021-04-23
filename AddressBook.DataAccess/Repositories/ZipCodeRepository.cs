using AddressBook.Common.Repositories;
using AddressBook.DataAccess.Entities;
using AddressBook.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace AddressBook.DataAccess.Repositories
{
    public class ZipCodeRepository : EntityFrameworkGenericRepository<ZipCode>, IZipCodeRepository
    {
        public ZipCodeRepository(
            AddressBookContext dbContext,
            ILoggerFactory loggerFactory)
            : base(dbContext, loggerFactory)
        {
        }
    }
}
