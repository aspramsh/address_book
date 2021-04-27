using AddressBook.Api.V1.Models.RequestModels;
using AddressBook.Api.V1.Models.ViewModels;
using AddressBook.Business.Facades.Interfaces;
using AddressBook.Business.Models;
using AddressBook.Business.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Api.V1.Controllers
{
    public class StatesController : BaseController
    {
        private readonly IStateFacade _stateFacade;

        public StatesController(
            ILogger<StatesController> logger,
            IMapper mapper,
            IStateFacade stateFacade)
            : base(logger, mapper)
        {
            _stateFacade = stateFacade;
        }

        [HttpGet("{countryId}")]
        public async Task<ActionResult<List<StateViewModel>>> GetAsync(
            int countryId,
            CancellationToken cancellationToken)
        {
            var states = await _stateFacade.GetByCountryAsync(countryId, cancellationToken);

            return Mapper.Map<List<StateViewModel>>(states);
        }

        [HttpPost]
        public async Task<ActionResult<StateViewModel>> AddAsync(
            StateCreateRequestModel model,
            CancellationToken cancellationToken)
        {
            var stateModel = Mapper.Map<StateModel>(model);

            var created = await _stateFacade.CreateAsync(stateModel, cancellationToken);
            
            return Mapper.Map<StateViewModel>(created);
        }
    }
}
