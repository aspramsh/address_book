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
    public class ZipCodesController : BaseController
    {
        private readonly IZipCodeFacade _zipCodeFacade;

        public ZipCodesController(
            ILogger<ZipCodesController> logger,
            IMapper mapper,
            IZipCodeFacade zipCodeFacade)
            : base(logger, mapper)
        {
            _zipCodeFacade = zipCodeFacade;
        }

        [HttpGet("countryId={countryId}/cityId={cityId}")]
        public async Task<ActionResult<List<ZipCodeViewModel>>> GetAsync(
            int countryId,
            int cityId,
            CancellationToken cancellationToken)
        {
            var zipCodes = await _zipCodeFacade.GetByAsync(countryId, cityId, cancellationToken);

            return Mapper.Map<List<ZipCodeViewModel>>(zipCodes);
        }

        [HttpPost]
        public async Task<ActionResult<ZipCodeViewModel>> AddAsync(
            ZipCodeCreateRequestModel model,
            CancellationToken cancellationToken)
        {
            var zipCodeModel = Mapper.Map<ZipCodeModel>(model);

            var created = await _zipCodeFacade.CreateAsync(zipCodeModel, cancellationToken);

            return Mapper.Map<ZipCodeViewModel>(created);
        }
    }
}
