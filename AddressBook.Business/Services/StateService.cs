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
        private readonly IMapper _mapper;

        private readonly IStateRepository _stateRepository;

        private readonly IPositionStackHttpClient _positionStackHttpClient;

        public StateService(
            IMapper mapper,
            ILoggerFactory loggerFactory,
            IStateRepository entityRepository,
            IPositionStackHttpClient positionStackHttpClient)
            : base(mapper, loggerFactory, entityRepository)
        {
            _mapper = mapper;
            _stateRepository = entityRepository;
            _positionStackHttpClient = positionStackHttpClient;
        }

        public async Task<StateModel> CreateSingleAsync(
            StateModel model,
            CancellationToken cancellationToken)
        {
            var geolocations = await _positionStackHttpClient.GetPlaceAsync(model.Name, cancellationToken);

            var state = geolocations.FirstOrDefault(x => x.Type == LocationType.region
            && x.Name?.ToLower() == model.Name.ToLower()
            && x.Country?.ToLower() == model.Country?.Name);

            if (state == default)
            {
                throw new BadRequestException("State name is invalid.");
            }

            model.Code = state.RegionCode;

            var entity = _mapper.Map<State>(model);
            entity.Country = default;

            var created = await _stateRepository.InsertAsync(entity, cancellationToken);
            await _stateRepository.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<StateModel>(created);
            result.Country = model.Country;

            return result;
        }
    }
}
