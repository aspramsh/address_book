using AddressBook.Api.V1.Models.RequestModels;
using AddressBook.Api.V1.Models.ViewModels;
using AddressBook.Business.Facades.Interfaces;
using AddressBook.Business.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Api.V1.Controllers
{
    public class CitiesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICityFacade _cityFacade;

        public CitiesController(
            ILogger<CitiesController> logger,
            IMapper mapper,
            ICityFacade cityFacade)
            : base(logger, mapper)
        {
            _mapper = mapper;
            _cityFacade = cityFacade;
        }

        [HttpPost]
        public async Task<ActionResult<CityViewModel>> AddAsync(
            CityCreateRequestModel model,
            CancellationToken cancellationToken)
        {
            var cityModel = _mapper.Map<CityModel>(model);

            var created = await _cityFacade.CreateAsync(cityModel, cancellationToken);

            return _mapper.Map<CityViewModel>(created);
        }
    }
}
