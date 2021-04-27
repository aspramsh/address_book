using AddressBook.Business.Models;
using AddressBook.Business.Services.Interfaces;
using AddressBook.Common.Services;
using AddressBook.DataAccess.Entities;
using AddressBook.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task<ZipCodeModel> CreateSingleAsync(
            ZipCodeModel model,
            CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<ZipCode>(model);
            entity.Country = default;
            entity.State = default;
            entity.City = default;

            var created = await EntityRepository.InsertAsync(entity, cancellationToken);
            await EntityRepository.SaveChangesAsync(cancellationToken);

            var result = Mapper.Map<ZipCodeModel>(created);
            result.Country = model.Country;
            result.State = model.State;
            result.City = model.City;

            return result;
        }
    }
}
