using AddressBook.Common.Repositories;
using AddressBook.DataAccess.Entities;
using AddressBook.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace AddressBook.DataAccess.Repositories
{
    public class PhoneNumberRepository : EntityFrameworkGenericRepository<PhoneNumber>, IPhoneNumberRepository
    {
        public PhoneNumberRepository(
            AddressBookContext dbContext,
            ILoggerFactory loggerFactory)
            : base(dbContext, loggerFactory)
        {
        }
    }
}
