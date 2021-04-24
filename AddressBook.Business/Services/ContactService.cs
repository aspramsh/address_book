using AddressBook.Business.Models;
using AddressBook.Business.Services.Interfaces;
using AddressBook.Common.Services;
using AddressBook.DataAccess.Entities;
using AddressBook.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AddressBook.Business.Services
{
    public class ContactService : GenericService<ContactModel, Contact>, IContactService
    {
        public ContactService(
            IMapper mapper,
            ILoggerFactory loggerFactory,
            IContactRepository entityRepository)
            : base(mapper, loggerFactory, entityRepository)
        {
        }
    }
}
