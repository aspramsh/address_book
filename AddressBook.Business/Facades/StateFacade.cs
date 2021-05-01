using AddressBook.Business.Facades.Interfaces;
using AddressBook.Business.Models;
using AddressBook.Business.Services.Interfaces;
using AddressBook.Common.Includable;
using AddressBook.Common.Mvc.Exceptions;
using AddressBook.DataAccess.Entities;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
            string searchValue,
            CancellationToken cancellationToken)
        {
            var included = new Func<IIncludable<State>, IIncludable>(x => x.Include(i => i.Country));

            Expression<Func<State, bool>> predicate = x => x.CountryId == countryId;
            
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                predicate = predicate.And(x => x.Name.ToLower().Contains(searchValue.ToLower()));
            }

            var states = await _stateService.FindByAsync(predicate, included, cancellationToken: cancellationToken);

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
