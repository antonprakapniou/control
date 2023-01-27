using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
    public class MeasuringService : GenericService<MeasuringVM, Measuring>,IMeasuringService
    {
        private readonly ILogger<GenericService<MeasuringVM, Measuring>> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Measuring> _repository;

        public MeasuringService(
            ILogger<GenericService<MeasuringVM, Measuring>> logger,
            IMapper mapper,
            IGenericRepository<Measuring> repository)
            : base(logger, mapper, repository)
        {
            _logger=logger;
            _mapper=mapper;
            _repository=repository;
        }

        public override async Task<IEnumerable<MeasuringVM>> GetAllAsync()
        {
            var models = await _repository.GetAllByAsync();

            if (models is null)
            {
                string errorMessage = $"'{models!.GetType().Name}' collection not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            var orderModels = models.OrderBy(_ => _.Code);
            var viewModels = _mapper.Map<IEnumerable<MeasuringVM>>(orderModels);
            return viewModels;
        }
    }
}
