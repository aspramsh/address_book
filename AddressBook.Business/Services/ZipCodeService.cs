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
        private readonly IMapper _mapper;

        private readonly IZipCodeRepository _zipCodeRepository;

        public ZipCodeService(
            IMapper mapper,
            ILoggerFactory loggerFactory,
            IZipCodeRepository entityRepository)
            : base(mapper, loggerFactory, entityRepository)
        {
            _mapper = mapper;
            _zipCodeRepository = entityRepository;
        }

        public async Task<ZipCodeModel> CreateSingleAsync(
            ZipCodeModel model,
            CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ZipCode>(model);
            entity.Country = default;
            entity.State = default;
            entity.City = default;

            var created = await _zipCodeRepository.InsertAsync(entity, cancellationToken);
            await _zipCodeRepository.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<ZipCodeModel>(created);
            result.Country = model.Country;
            result.State = model.State;
            result.City = model.City;

            return result;
        }
    }
}
