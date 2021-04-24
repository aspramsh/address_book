using AddressBook.Business.Models;
using AddressBook.Business.Services.Interfaces;
using AddressBook.Common.Services;
using AddressBook.DataAccess.Entities;
using AddressBook.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AddressBook.Business.Services
{
    public class CityService : GenericService<CityModel, City>, ICityService
    {
        public CityService(
            IMapper mapper,
            ILoggerFactory loggerFactory,
            ICityRepository entityRepository)
            : base(mapper, loggerFactory, entityRepository)
        {
        }
    }
}
