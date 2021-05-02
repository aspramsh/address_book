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
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Services
{
    public class StateService : GenericService<StateModel, State>, IStateService
    {
        private readonly IPositionStackHttpClient _positionStackHttpClient;

        public StateService(
            IMapper mapper,
            ILoggerFactory loggerFactory,
            IStateRepository entityRepository,
            IPositionStackHttpClient positionStackHttpClient)
            : base(mapper, loggerFactory, entityRepository)
        {
            _positionStackHttpClient = positionStackHttpClient;
        }

        public async Task<StateModel> CreateSingleAsync(
            StateModel model,
            CancellationToken cancellationToken)
        {
            try
            {
                var geolocations = await _positionStackHttpClient.GetPlaceAsync(model.Name, cancellationToken);

                var state = geolocations.FirstOrDefault(x => x.Type == LocationType.region
                && x.Name?.ToLower() == model.Name.ToLower()
                && x.Country?.ToLower() == model.Country?.Name?.ToLower());

                if (state == default)
                {
                    throw new BadRequestException("State name is invalid.");
                }

                model.Code = state.RegionCode;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unable to validate state");
            }

            var entity = Mapper.Map<State>(model);
            entity.Country = default;

            var created = await EntityRepository.InsertAsync(entity, cancellationToken);
            await EntityRepository.SaveChangesAsync(cancellationToken);

            var result = Mapper.Map<StateModel>(created);
            result.Country = model.Country;

            return result;
        }
    }
}
