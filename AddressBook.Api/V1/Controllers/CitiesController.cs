using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AddressBook.Api.V1.Controllers
{
    public class CitiesController : BaseController
    {
        public CitiesController(
            ILogger<CitiesController> logger,
            IMapper mapper)
            : base(logger, mapper)
        {
        }
    }
}
