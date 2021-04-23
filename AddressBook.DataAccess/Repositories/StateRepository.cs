using AddressBook.Common.Repositories;
using AddressBook.DataAccess.Entities;
using AddressBook.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace AddressBook.DataAccess.Repositories
{
    public class StateRepository : EntityFrameworkGenericRepository<State>, IStateRepository
    {
        public StateRepository(
            AddressBookContext dbContext,
            ILoggerFactory loggerFactory)
            : base(dbContext, loggerFactory)
        {
        }
    }
}
