using AddressBook.Business.Facades.Interfaces;
using AddressBook.Business.Models;
using AddressBook.Business.Services.Interfaces;
using AddressBook.Common.Mvc.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Facades
{
    public class ContactFacade : IContactFacade
    {
        private readonly IContactService _contactService;

        private readonly IZipCodeService _zipCodeService;

        public ContactFacade(
            IContactService contactService,
            IZipCodeService zipCodeService)
        {
            _contactService = contactService;
            _zipCodeService = zipCodeService;
        }

        public async Task<ContactModel> CreateAsync(
            ContactModel model,
            CancellationToken cancellationToken)
        {
            var existingZipCode = await _zipCodeService.GetFirstOrDefaultAsync(
                x => x.Id == model.ZipCodeId, cancellationToken: cancellationToken);
            var existingContact = await _contactService.GetFirstOrDefaultAsync(
                x => x.Email.ToLower() == model.Email.ToLower(),
                cancellationToken: cancellationToken);

            if (existingContact != default)
            {
                throw new BadRequestException($"{model.Email} contact already exists");
            }

            model.ZipCode = existingZipCode ?? throw new NotFoundException($"Zip code ID {model.ZipCodeId} does not exist.");
            var created = await _contactService.CreateSingleAsync(model, cancellationToken);

            return created;
        }
    }
}
