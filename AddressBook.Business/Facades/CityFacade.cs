using AddressBook.Business.Facades.Interfaces;
using AddressBook.Business.Models;
using AddressBook.Business.Services.Interfaces;
using AddressBook.Common.Includable;
using AddressBook.Common.Mvc.Exceptions;
using AddressBook.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Facades
{
    public class CityFacade : ICityFacade
    {
        private readonly ICountryService _countryService;

        private readonly IStateService _stateService;

        private readonly ICityService _cityService;

        public CityFacade(
            ICountryService countryService,
            IStateService stateService,
            ICityService cityService)
        {
            _countryService = countryService;
            _stateService = stateService;
            _cityService = cityService;
        }

        public async Task<List<CityModel>> GetByCountryAsync(
            int countryId,
            CancellationToken cancellationToken)
        {
            var included = new Func<IIncludable<City>, IIncludable>(x => x
            .Include(i => i.Country)
            .Include(i => i.State));

            var cities = await _cityService.FindByAsync(x => x.CountryId == countryId, included, cancellationToken: cancellationToken);

            return cities;
        }

        public async Task<CityModel> CreateAsync(
            CityModel model,
            CancellationToken cancellationToken)
        {
            var existingCountry = await _countryService.GetFirstOrDefaultAsync(
                x => x.Id == model.CountryId, cancellationToken: cancellationToken);
            
            if (model.StateId != null)
            {
                var existingState = await _stateService.GetFirstOrDefaultAsync(
                x => x.Id == model.StateId,
                cancellationToken: cancellationToken);
                if (existingState == default || existingState.CountryId != model.StateId)
                {
                    throw new BadRequestException("Invalid state name");
                }
                model.State = existingState;
            }

            var existingCity = await _cityService.GetFirstOrDefaultAsync(x => x.Name == model.Name
            && x.StateId == model.StateId
            && x.CountryId == model.CountryId, cancellationToken: cancellationToken);


            if (existingCity != default)
            {
                throw new BadRequestException($"{model.Name} city already exists");
            }

            model.Country = existingCountry ?? throw new NotFoundException($"countryId {model.CountryId} does not exist.");
            var created = await _cityService.CreateSingleAsync(model, cancellationToken);

            return created;
        }
    }
}
