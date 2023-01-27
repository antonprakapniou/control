using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
    public sealed class OwnerService : GenericService<OwnerVM, Owner>,IOwnerService
    {
        private readonly ILogger<GenericService<OwnerVM, Owner>> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Owner> _repository;

        public OwnerService(
            ILogger<GenericService<OwnerVM, Owner>> logger,
            IMapper mapper,
            IGenericRepository<Owner> repository)
            : base(logger, mapper, repository)
        {
            _logger=logger;
            _mapper=mapper;
            _repository=repository;
        }

        public override async Task<IEnumerable<OwnerVM>> GetAllAsync()
        {
            var models = await _repository.GetAllByAsync();            

            if (models is null)
            {
                string errorMessage = $"'{models!.GetType().Name}' collection not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            var orderModels = models.OrderBy(_ => _.Production).ThenBy(_=>_.Shop);
            var viewModels = _mapper.Map<IEnumerable<OwnerVM>>(orderModels);
            return viewModels;
        }
    }
}
