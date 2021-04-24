using AddressBook.Business.Models;
using AddressBook.Business.Services.Interfaces;
using AddressBook.Common.Services;
using AddressBook.DataAccess.Entities;
using AddressBook.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AddressBook.Business.Services
{
    public class StateService : GenericService<StateModel, State>, IStateService
    {
        public StateService(
            IMapper mapper,
            ILoggerFactory loggerFactory,
            IStateRepository entityRepository)
            : base(mapper, loggerFactory, entityRepository)
        {
        }
    }
}
