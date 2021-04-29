using AddressBook.Business.Models;
using AddressBook.Business.Services.Interfaces;
using AddressBook.Common.Services;
using AddressBook.DataAccess.Entities;
using AddressBook.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Business.Services
{
    public class ContactService : GenericService<ContactModel, Contact>, IContactService
    {
        public ContactService(
            IMapper mapper,
            ILoggerFactory loggerFactory,
            IContactRepository entityRepository)
            : base(mapper, loggerFactory, entityRepository)
        {
        }

        public async Task<ContactModel> CreateSingleAsync(
            ContactModel model,
            CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<Contact>(model);
            entity.ZipCode = default;

            var created = await EntityRepository.InsertAsync(entity, cancellationToken);
            await EntityRepository.SaveChangesAsync(cancellationToken);

            var result = Mapper.Map<ContactModel>(created);
            result.ZipCode = model.ZipCode;

            return result;
        }
    }
}
