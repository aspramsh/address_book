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
    public class ZipCodeFacade : IZipCodeFacade
    {
        private readonly IZipCodeService _zipCodeService;

        private readonly ICityService _cityService;

        public ZipCodeFacade(
            IZipCodeService zipCodeService,
            ICityService cityService)
        {
            _zipCodeService = zipCodeService;
            _cityService = cityService;
        }

        public async Task<List<ZipCodeModel>> GetByAsync(
            int countryId,
            int cityId,
            CancellationToken cancellationToken)
        {
            var included = new Func<IIncludable<ZipCode>, IIncludable>(x => x
            .Include(i => i.State)
            .Include(i => i.City)
            .ThenInclude(i => i.Country));

            var states = await _zipCodeService.FindByAsync(x => x.CountryId == countryId && x.CityId == cityId, included, cancellationToken: cancellationToken);

            return states;
        }

        public async Task<ZipCodeModel> CreateAsync(
            ZipCodeModel model,
            CancellationToken cancellationToken)
        {
            var included = new Func<IIncludable<City>, IIncludable>(x => x.Include(i => i.State).Include(i => i.Country));
            var existingCity = await _cityService.GetFirstOrDefaultAsync(
                x => x.Id == model.CityId, included, cancellationToken: cancellationToken);

            if (existingCity == default)
            {
                throw new BadRequestException($"Invalid {nameof(model.CityId)}");
            }

            model.CountryId = existingCity.CountryId;
            model.Country = existingCity.Country;
            model.StateId = existingCity.StateId;
            model.State = existingCity.State;
            model.City = existingCity;

            var created = await _zipCodeService.CreateSingleAsync(model, cancellationToken);

            return created;
        }
    }
}
