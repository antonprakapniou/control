using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
	public sealed class PeriodService:GenericService<PeriodVM,Period>,IPeriodService
	{
        private readonly ILogger<GenericService<PeriodVM, Period>> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Period> _repository;

        public PeriodService(
            ILogger<GenericService<PeriodVM, Period>> logger,
            IMapper mapper,
            IGenericRepository<Period> repository)
            : base(logger, mapper, repository)
        {
            _logger=logger;
            _mapper=mapper;
            _repository=repository;
        }

        public override async Task<IEnumerable<PeriodVM>> GetAllAsync()
        {
            var models = await _repository
                .GetAllByAsync();

            if (models is null)
            {
                string errorMessage = $"'{models!.GetType().Name}' collection not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            var orderModels = models.OrderBy(_ => _.Month);
            var viewModels = _mapper.Map<IEnumerable<PeriodVM>>(orderModels);
            return viewModels;
        }

        public override async Task CreateAsync(PeriodVM viewModel)
        {
            if (int.TryParse(viewModel.Name, out int month))
            {
                var model = _mapper.Map<Period>(viewModel);
                model.Month= month;
                await _repository.CreateAsync(model);
            }

            else throw  new InvalidValueException("Invalid 'Period' value. It must be '012', for example");            
        }
    }
}
