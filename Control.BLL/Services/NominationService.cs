using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
    public sealed class NominationService : GenericService<NominationVM, Nomination>,INominationService
    {
        private readonly ILogger<GenericService<NominationVM, Nomination>> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Nomination> _repository;

        public NominationService(
            ILogger<GenericService<NominationVM, Nomination>> logger,
            IMapper mapper,
            IGenericRepository<Nomination> repository)
            : base(logger, mapper, repository)
        {
            _logger=logger;
            _mapper=mapper;
            _repository=repository;
        }

        public override async Task<IEnumerable<NominationVM>> GetAllAsync()
        {
            var models = await _repository.GetAllByAsync();

            if (models is null)
            {
                string errorMessage = $"'{models!.GetType().Name}' collection not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            var orderModels = models.OrderBy(_ => _.Name);
            var viewModels = _mapper.Map<IEnumerable<NominationVM>>(orderModels);
            return viewModels;
        }
    }
}
