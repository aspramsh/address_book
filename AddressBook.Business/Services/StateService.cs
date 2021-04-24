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
    public class StateService : GenericService<StateModel, State>, IStateService
    {
        private readonly IMapper _mapper;

        private readonly IStateRepository _stateRepository;

        public StateService(
            IMapper mapper,
            ILoggerFactory loggerFactory,
            IStateRepository entityRepository)
            : base(mapper, loggerFactory, entityRepository)
        {
            _mapper = mapper;
            _stateRepository = entityRepository;
        }

        public async Task<StateModel> CreateSingleAsync(
            StateModel model,
            CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<State>(model);

            var created = await _stateRepository.InsertAsync(entity, cancellationToken);
            await _stateRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<StateModel>(created);
        }
    }
}
