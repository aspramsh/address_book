using AddressBook.Api.V1.Models.ViewModels;
using AddressBook.Business.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
            CancellationToken cancellationToken)
        {
            var countries = await _countryService.GetAllAsync(cancellationToken: cancellationToken);

            return Mapper.Map<List<CountryViewModel>>(countries);
        }
    }
}
