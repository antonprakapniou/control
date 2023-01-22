using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
	public sealed class PositionService:IPositionService
	{
		private const int _defaultPeriod = 12;
		private readonly ILogger<PositionService> _logger;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public PositionService(
			ILogger<PositionService> logger,
			IMapper mapper,
			IUnitOfWork unitOfWork)
		{
			_logger=logger;
			_mapper=mapper;
			_unitOfWork=unitOfWork;
		}

		public async Task<IEnumerable<PositionVM>> GetAsync()
		{
			var models = await _unitOfWork.Positions.GetAllAsync(
					include: query => query
						.Include(_=>_.Category)
						.Include(_ => _.Measuring)
						.Include(_ => _.Nomination)
						.Include(_ => _.Operation)
						.Include(_ => _.Owner)
						.Include(_ => _.Period)!,
					isTracking: false);

			if (models==null)
			{
				string errorMessage = $"{models!.GetType().Name} collection not found ";
				_logger.LogError(errorMessage);
				throw new ObjectNotFoundException(errorMessage);
			}

			else
			{
				var modelsVM = _mapper.Map<IEnumerable<PositionVM>>(models);
				return modelsVM;
			}
		}

		public async Task<PositionVM> GetByIdAsync(Guid id)
		{
			var models = await _unitOfWork.Positions
				.GetAllAsync(
					expression: _ => _.PositionId.Equals(id),
					include:query=>query
						.Include(_ => _.Category)
                        .Include(_ => _.Measuring)
						.Include(_ => _.Nomination)
						.Include(_ => _.Operation)
						.Include(_ => _.Owner)
						.Include(_ => _.Period)!,
					isTracking: false);

			var model = models.FirstOrDefault();

			if (model==null)
			{
				string errorMessage = $"{model!.GetType().Name} model with id: {id} not found ";
				_logger.LogError(errorMessage);
				throw new ObjectNotFoundException(errorMessage);
			}

			else
			{
				var modelVM = _mapper.Map<PositionVM>(model);
				return modelVM;
			}
		}

		public async Task CreateAsync(PositionVM vm)
		{
			var model = _mapper.Map<Position>(vm);
			var periods = await _unitOfWork.Periods.GetAllAsync(
					expression: _ => _.PeriodId.Equals(vm.PeriodId),
					isTracking:false);

			var period = periods.FirstOrDefault();

			if (model.Period==null) model.NextDate=model.PreviousDate.AddMonths(_defaultPeriod);
			else model.NextDate = model.PreviousDate.AddMonths(model.Period.Month);

			model.Status=SetStatus(model.NextDate);
            model.Created=DateTime.Now;

			_unitOfWork.Positions.Create(model);
			await _unitOfWork.SaveAsync();
		}

        public async Task UpdateAsync(PositionVM vm)
        {
            var model = _mapper.Map<Owner>(vm);
            var models = await _unitOfWork.Owners
                .GetAllAsync(
                    expression: _ => _.OwnerId.Equals(model.OwnerId),
                    isTracking: false);

            if (models==null)
            {
                string errorMessage = $"{model!.GetType().Name} model with id: {model.OwnerId} not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            else
            {
                _unitOfWork.Owners.Update(model);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {	
            var model = await _unitOfWork.Positions.GetOneAsync(
                expression: _ => _.PositionId.Equals(id),
                isTracking: false);

            if (model==null)
            {
                string errorMessage = $"{model!.GetType().Name} model with id: {id} not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            else
            {
                _unitOfWork.Positions.Delete(model);
                await _unitOfWork.SaveAsync();
            }
        }

        private static StatusEnum SetStatus(DateTime nextDate)
        {
            DateTime currentDate = DateTime.Now;

            if (currentDate>nextDate) return StatusEnum.Invalid;
            else if (currentDate.AddMonths(2)>nextDate&&currentDate.Month.Equals(nextDate.Month)) return StatusEnum.CurrentMonthControl;
            else if (currentDate.AddMonths(2)>nextDate&&currentDate.AddMonths(1).Month.Equals(nextDate.Month)) return StatusEnum.CurrentMonthControl;
            else return StatusEnum.Valid;
        }
    }
}
