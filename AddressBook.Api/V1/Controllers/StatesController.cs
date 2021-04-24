using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AddressBook.Api.V1.Controllers
{
    public class StatesController : BaseController
    {
        public StatesController(
            ILogger logger,
            IMapper mapper)
            : base(logger, mapper)
        {
        }
    }
}
