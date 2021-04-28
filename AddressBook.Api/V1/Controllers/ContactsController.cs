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
    public class ContactsController : BaseController
    {
        private readonly IContactFacade _contactFacade;

        public ContactsController(
            ILogger<ContactsController> logger,
            IMapper mapper,
            IContactFacade contactFacade)
            : base(logger, mapper)
        {
            _contactFacade = contactFacade;
        }

        [HttpPost]
        public async Task<ActionResult<ContactViewModel>> AddAsync(
            ContactCreateRequestModel model,
            CancellationToken cancellationToken)
        {
            var contactModel = Mapper.Map<ContactModel>(model);

            var created = await _contactFacade.CreateAsync(contactModel, cancellationToken);

            return Mapper.Map<ContactViewModel>(created);
        }
    }
}
