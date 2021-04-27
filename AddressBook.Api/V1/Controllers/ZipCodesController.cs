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
    public class ZipCodesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IZipCodeFacade _zipCodeFacade;

        public ZipCodesController(
            ILogger<ZipCodesController> logger,
            IMapper mapper,
            IZipCodeFacade zipCodeFacade)
            : base(logger, mapper)
        {
            _mapper = mapper;
            _zipCodeFacade = zipCodeFacade;
        }

        [HttpPost]
        public async Task<ActionResult<ZipCodeViewModel>> AddAsync(
            ZipCodeCreateRequestModel model,
            CancellationToken cancellationToken)
        {
            var zipCodeModel = _mapper.Map<ZipCodeModel>(model);

            var created = await _zipCodeFacade.CreateAsync(zipCodeModel, cancellationToken);

            return _mapper.Map<ZipCodeViewModel>(created);
        }
    }
}
