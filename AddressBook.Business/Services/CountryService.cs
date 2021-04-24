using AddressBook.Business.Models;
using AddressBook.Business.Services.Interfaces;
using AddressBook.Common.Services;
using AddressBook.DataAccess.Entities;
using AddressBook.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AddressBook.Business.Services
{
    public class CountryService : GenericService<CountryModel, Country>, ICountryService
    {
        public CountryService(
            IMapper mapper,
            ILoggerFactory loggerFactory,
            ICountryRepository entityRepository)
            : base(mapper, loggerFactory, entityRepository)
        {
        }
    }
}
