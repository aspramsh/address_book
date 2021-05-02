using AddressBook.Api.V1.Models.ViewModels;
using AddressBook.Business.Services.Interfaces;
using AddressBook.DataAccess.Entities;
using AutoMapper;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Api.V1.Controllers
{
    public class CountriesController : BaseController
    {
        private readonly ICountryService _countryService;

        public CountriesController(
            ILogger<CountriesController> logger,
            IMapper mapper,
            ICountryService countryService)
            : base(logger, mapper)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CountryViewModel>>> GetAsync(
            string searchValue = default,
            CancellationToken cancellationToken = default)
        {
            Expression<Func<Country, bool>> predicate = x => true;
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                predicate = predicate.And(x => x.Name.ToLower().Contains(searchValue.ToLower()));
            }

            var countries = await _countryService.FindByAsync(predicate, cancellationToken: cancellationToken);

            return Mapper.Map<List<CountryViewModel>>(countries);
        }
    }
}
