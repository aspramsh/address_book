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
    public class StateFacade : IStateFacade
    {
        private readonly ICountryService _countryService;

        private readonly IStateService _stateService;

        public StateFacade(
            ICountryService countryService,
            IStateService stateService)
        {
            _countryService = countryService;
            _stateService = stateService;
        }

        public async Task<List<StateModel>> GetByCountryAsync(
            int countryId,
            CancellationToken cancellationToken)
        {
            var included = new Func<IIncludable<State>, IIncludable>(x => x.Include(i => i.Country));

            var states = await _stateService.FindByAsync(x => x.CountryId == countryId, included, cancellationToken: cancellationToken);

            return states;
        }

        public async Task<StateModel> CreateAsync(
            StateModel model,
            CancellationToken cancellationToken)
        {
            var existingCountry = await _countryService.GetFirstOrDefaultAsync(
                x => x.Id == model.CountryId, cancellationToken: cancellationToken);
            var existingState = await _stateService.GetFirstOrDefaultAsync(
                x => x.Name == model.Name
                && x.CountryId == model.CountryId,
                cancellationToken: cancellationToken);

            if (existingState != default)
            {
                throw new BadRequestException($"{model.Name} state already exists");
            }

            model.Country = existingCountry ?? throw new NotFoundException($"countryId {model.CountryId} does not exist.");
            var created = await _stateService.CreateSingleAsync(model, cancellationToken);

            return created;
        }
    }
}
