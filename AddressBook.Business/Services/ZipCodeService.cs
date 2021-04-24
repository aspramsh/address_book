using AddressBook.Business.Models;
using AddressBook.Business.Services.Interfaces;
using AddressBook.Common.Services;
using AddressBook.DataAccess.Entities;
using AddressBook.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AddressBook.Business.Services
{
    public class ZipCodeService : GenericService<ZipCodeModel, ZipCode>, IZipCodeService
    {
        public ZipCodeService(
            IMapper mapper,
            ILoggerFactory loggerFactory,
            IZipCodeRepository entityRepository)
            : base(mapper, loggerFactory, entityRepository)
        {
        }
    }
}
