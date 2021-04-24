using AddressBook.Api.V1.Models.RequestModels;
using AddressBook.Api.V1.Models.ViewModels;
using AddressBook.Business.Models;
using AddressBook.Business.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Api.V1.Controllers
{
    public class StatesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IStateService _stateService;

        public StatesController(
            ILogger<StatesController> logger,
            IMapper mapper,
            IStateService stateService)
            : base(logger, mapper)
        {
            _mapper = mapper;
            _stateService = stateService;
        }

        [HttpPost]
        public async Task<ActionResult<StateViewModel>> AddAsync(
            StateCreateRequestModel model,
            CancellationToken cancellationToken)
        {
            var stateModel = _mapper.Map<StateModel>(model);

            var created = await _stateService.CreateSingleAsync(stateModel, cancellationToken);
            
            return _mapper.Map<StateViewModel>(created);
        }
    }
}
