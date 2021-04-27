using AddressBook.Business.Geocoders.Interfaces;
using AddressBook.Business.Models;
using AddressBook.Business.Models.Enums;
using AddressBook.Business.Services.Interfaces;
using AddressBook.Common.Mvc.Exceptions;
using AddressBook.Common.Services;
using AddressBook.DataAccess.Entities;
using AddressBook.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Services
{
    public class CityService : GenericService<CityModel, City>, ICityService
    {
        private readonly IPositionStackHttpClient _positionStackHttpClient;

        public CityService(
            IMapper mapper,
            ILoggerFactory loggerFactory,
            ICityRepository entityRepository,
            IPositionStackHttpClient positionStackHttpClient)
            : base(mapper, loggerFactory, entityRepository)
        {
            _positionStackHttpClient = positionStackHttpClient;
        }

        public async Task<CityModel> CreateSingleAsync(
            CityModel model,
            CancellationToken cancellationToken)
        {
            var geolocations = await _positionStackHttpClient.GetPlaceAsync(model.Name, cancellationToken);

            var city = geolocations.FirstOrDefault(x => x.Type == LocationType.locality
            && x.Name?.ToLower() == model.Name.ToLower()
            && x.Country?.ToLower() == model.Country?.Name?.ToLower());

            if (city == default)
            {
                throw new BadRequestException("City name is invalid.");
            }

            model.Longitude = city.Longitude;
            model.Latitude = city.Latitude;

            var entity = Mapper.Map<City>(model);
            entity.Country = default;
            entity.State = default;

            var created = await EntityRepository.InsertAsync(entity, cancellationToken);
            await EntityRepository.SaveChangesAsync(cancellationToken);

            var result = Mapper.Map<CityModel>(created);
            result.Country = model.Country;
            result.State = model.State;

            return result;
        }
    }
}
