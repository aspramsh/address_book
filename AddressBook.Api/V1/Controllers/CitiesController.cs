using AddressBook.Api.V1.Models.RequestModels;
using AddressBook.Api.V1.Models.ViewModels;
using AddressBook.Business.Facades.Interfaces;
using AddressBook.Business.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Api.V1.Controllers
{
    public class CitiesController : BaseController
    {
        private readonly ICityFacade _cityFacade;

        public CitiesController(
            ILogger<CitiesController> logger,
            IMapper mapper,
            ICityFacade cityFacade)
            : base(logger, mapper)
        {
            _cityFacade = cityFacade;
        }

        [HttpGet("{countryId}")]
        public async Task<ActionResult<List<CityViewModel>>> GetAsync(
            int countryId,
            string searchValue,
            CancellationToken cancellationToken)
        {
            var cities = await _cityFacade.GetByCountryAsync(countryId, searchValue, cancellationToken);

            return Mapper.Map<List<CityViewModel>>(cities);
        }

        [HttpPost]
        public async Task<ActionResult<CityViewModel>> AddAsync(
            CityCreateRequestModel model,
            CancellationToken cancellationToken)
        {
            var cityModel = Mapper.Map<CityModel>(model);

            var created = await _cityFacade.CreateAsync(cityModel, cancellationToken);

            return Mapper.Map<CityViewModel>(created);
        }
    }
}
