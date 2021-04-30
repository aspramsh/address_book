using AddressBook.Business.Facades.Interfaces;
using AddressBook.Business.Models;
using AddressBook.Business.Services.Interfaces;
using AddressBook.Common.Includable;
using AddressBook.Common.Mvc.Exceptions;
using AddressBook.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Facades
{
    public class ContactFacade : IContactFacade
    {
        private readonly IContactService _contactService;

        private readonly IZipCodeService _zipCodeService;

        private static Func<IIncludable<Contact>, IIncludable> Included => _ =>_
        .Include(x => x.PhoneNumbers)
        .Include(x => x.ZipCode)
        .ThenInclude(x => x.City)
        .ThenInclude(x => x.Country);

        public ContactFacade(
            IContactService contactService,
            IZipCodeService zipCodeService)
        {
            _contactService = contactService;
            _zipCodeService = zipCodeService;
        }

        public async Task<ContactModel> GetAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var contact = await _contactService.GetFirstOrDefaultAsync(
                x => x.Id == id,
                Included,
                cancellationToken: cancellationToken);

            return contact;
        }

        public async Task<List<ContactModel>> GetListAsync(
            CancellationToken cancellationToken)
        {
            var contacts = await _contactService.GetAllAsync(Included, cancellationToken: cancellationToken);

            return contacts;
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

        public async Task UpdateAsync(
            ContactModel model,
            CancellationToken cancellationToken)
        {
            var existingZipCode = await _zipCodeService.GetFirstOrDefaultAsync(
                x => x.Id == model.ZipCodeId, cancellationToken: cancellationToken);
            var existingContact = await _contactService.GetFirstOrDefaultAsync(
                x => x.Id == model.Id,
                cancellationToken: cancellationToken);

            if (existingContact == default)
            {
                throw new BadRequestException($"{model.Email} contact does not exist");
            }

            model.ZipCode = existingZipCode ?? throw new NotFoundException($"Zip code ID {model.ZipCodeId} does not exist.");
            
            _contactService.Update(model);
            await _contactService.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var existingContact = await _contactService.GetFirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken: cancellationToken);

            if (existingContact == default)
            {
                throw new BadRequestException($"{id} contact does not exist");
            }

            _contactService.Delete(existingContact);
            await _contactService.SaveChangesAsync(cancellationToken);
        }
    }
}
