using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AddressBook.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [EnableCors("AddressBook")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected ILogger Logger { get; }
        protected IMapper Mapper { get; }

        public BaseController(ILogger logger, IMapper mapper) 
        {
            Logger = logger;
            Mapper = mapper;
        }
    }
}
