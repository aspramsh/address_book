using AddressBook.Api.V1.Models.RequestModels;
using AddressBook.Api.V1.Models.ViewModels;
using AddressBook.Business.Facades.Interfaces;
using AddressBook.Business.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactViewModel>> GetListAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var contact = await _contactFacade.GetAsync(id, cancellationToken);

            if (contact == default)
            {
                return NotFound(new { Error = "Contact does not exist." });
            }

            return Mapper.Map<ContactViewModel>(contact);
        }

        // TODO: Add pagination
        [HttpGet]
        public async Task<ActionResult<List<ContactViewModel>>> GetListAsync(CancellationToken cancellationToken)
        {
            var contacts = await _contactFacade.GetListAsync(cancellationToken);

            return Mapper.Map<List<ContactViewModel>>(contacts);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(
            [Range(1, int.MaxValue)] int id,
            ContactUpdateRequestModel model,
            CancellationToken cancellationToken)
        {
            if (id != model.Id)
            {
                return BadRequest(new { errorMessage = "IDs do not match." });
            }

            var contactModel = Mapper.Map<ContactModel>(model);

            await _contactFacade.UpdateAsync(contactModel, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ContactViewModel>> DeleteAsync(
            [Range(1, int.MaxValue)] int id,
            CancellationToken cancellationToken)
        {
            await _contactFacade.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
