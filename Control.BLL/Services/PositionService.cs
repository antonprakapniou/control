﻿using AutoMapper;
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
		private readonly ILogger<MeasuringService> _logger;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public PositionService(
			ILogger<MeasuringService> logger,
			IMapper mapper,
			IUnitOfWork unitOfWork)
		{
			_logger=logger;
			_mapper=mapper;
			_unitOfWork=unitOfWork;
		}

		public async Task<IEnumerable<PositionVM>> GetAsync()
		{
			var models = await _unitOfWork.Positions.GetAsync(
					include: query => query
						.Include(_ => _.Measuring)
						.Include(_ => _.Nomination)
						.Include(_ => _.Operation)
						.Include(_ => _.Owner)
						.Include(_ => _.Period)
						.Include(_ => _.Status)
						.Include(_ => _.Units)!,
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
				.GetAsync(
					expression: _ => _.UnitsId.Equals(id),
					include:query=>query
						.Include(_ => _.Measuring)
						.Include(_ => _.Nomination)
						.Include(_ => _.Operation)
						.Include(_ => _.Owner)
						.Include(_ => _.Period)
						.Include(_ => _.Status)
						.Include(_ => _.Units)!,
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
			_unitOfWork.Positions.Create(model);
			await _unitOfWork.SaveAsync();
		}

		public async Task UpdateAsync(PositionVM vm)
		{
			var id = vm.PositionId;
			var models = await _unitOfWork.Positions
				.GetAsync(
					expression: _ => _.PositionId.Equals(id),
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
				_unitOfWork.Positions.Update(model);
			}
		}

		public async Task DeleteAsync(PositionVM vm)
		{
			var id = vm.PositionId;
			var models = await _unitOfWork.Positions
				.GetAsync(
					expression: _ => _.PositionId.Equals(id),
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
				_unitOfWork.Positions.Delete(model);
			}
		}
	}
}
