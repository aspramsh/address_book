using AddressBook.Business.Facades.Interfaces;
using AddressBook.Business.Models;
using AddressBook.Business.Services.Interfaces;
using AddressBook.Common.Mvc.Exceptions;
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
