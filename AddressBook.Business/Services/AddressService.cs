using AddressBook.Business.Models;
using AddressBook.Business.Services.Interfaces;
using AddressBook.Common.Services;
using AddressBook.DataAccess.Entities;
using AddressBook.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AddressBook.Business.Services
{
    public class AddressService : GenericService<AddressModel, Address>, IAddressService
    {
        public AddressService(
            IMapper mapper,
            ILoggerFactory loggerFactory,
            IAddressRepository entityRepository)
            : base(mapper, loggerFactory, entityRepository)
        {
        }
    }
}
