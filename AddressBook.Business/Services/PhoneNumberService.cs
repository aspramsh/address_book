using AddressBook.Business.Models;
using AddressBook.Business.Services.Interfaces;
using AddressBook.Common.Services;
using AddressBook.DataAccess.Entities;
using AddressBook.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AddressBook.Business.Services
{
    public class PhoneNumberService : GenericService<PhoneNumberModel, PhoneNumber>, IPhoneNumberService
    {
        public PhoneNumberService(
            IMapper mapper,
            ILoggerFactory loggerFactory,
            IPhoneNumberRepository entityRepository)
            : base(mapper, loggerFactory, entityRepository)
        {
        }
    }
}
