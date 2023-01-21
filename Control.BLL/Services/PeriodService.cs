﻿using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
	public sealed class PeriodService:IPeriodService
	{
		private readonly ILogger<MeasuringService> _logger;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public PeriodService(
			ILogger<MeasuringService> logger,
			IMapper mapper,
			IUnitOfWork unitOfWork)
		{
			_logger=logger;
			_mapper=mapper;
			_unitOfWork=unitOfWork;
		}

		public async Task<IEnumerable<PeriodVM>> GetAsync()
		{
			var models = await _unitOfWork.Periods.GetAsync(isTracking: false);

			if (models==null)
			{
				string errorMessage = $"{models!.GetType().Name} collection not found ";
				_logger.LogError(errorMessage);
				throw new ObjectNotFoundException(errorMessage);
			}

			else
			{
				var modelsVM = _mapper.Map<IEnumerable<PeriodVM>>(models);
				return modelsVM;
			}
		}

		public async Task<PeriodVM> GetByIdAsync(Guid id)
		{
			var models = await _unitOfWork.Periods
				.GetAsync(
					expression: _ => _.PeriodId.Equals(id),
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
				var modelVM = _mapper.Map<PeriodVM>(model);
				return modelVM;
			}
		}

		public async Task CreateAsync(PeriodVM vm)
		{
			var model = _mapper.Map<Period>(vm);
			_unitOfWork.Periods.Create(model);
			await _unitOfWork.SaveAsync();
		}

		public async Task UpdateAsync(PeriodVM vm)
		{
			var id = vm.PeriodId;
			var models = await _unitOfWork.Periods
				.GetAsync(
					expression: _ => _.PeriodId.Equals(id),
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
				_unitOfWork.Periods.Update(model);
			}
		}

		public async Task DeleteAsync(PeriodVM vm)
		{
			var id = vm.PeriodId;
			var models = await _unitOfWork.Periods
				.GetAsync(
					expression: _ => _.PeriodId.Equals(id),
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
				_unitOfWork.Periods.Delete(model);
			}
		}
	}
}
