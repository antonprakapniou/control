using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
    public sealed class OperationService : GenericService<OperationVM, Operation>,IOperationService
    {
        private readonly ILogger<GenericService<OperationVM, Operation>> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Operation> _repository;

        public OperationService(
            ILogger<GenericService<OperationVM, Operation>> logger,
            IMapper mapper,
            IGenericRepository<Operation> repository)
            : base(logger, mapper, repository)
        {
            _logger=logger;
            _mapper=mapper;
            _repository=repository;
        }

        public override async Task<IEnumerable<OperationVM>> GetAllAsync()
        {
            var models = await _repository.GetAllByAsync();

            if (models is null)
            {
                string errorMessage = $"'{models!.GetType().Name}' collection not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            var orderModels = models.OrderBy(_ => _.Name);
            var viewModels = _mapper.Map<IEnumerable<OperationVM>>(orderModels);
            return viewModels;
        }
    }
}
