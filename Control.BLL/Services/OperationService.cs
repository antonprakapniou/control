﻿using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
	public sealed class OperationService:IOperationService
	{
		private readonly ILogger<OperationService> _logger;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public OperationService(
			ILogger<OperationService> logger,
			IMapper mapper,
			IUnitOfWork unitOfWork)
		{
			_logger=logger;
			_mapper=mapper;
			_unitOfWork=unitOfWork;
		}

		public async Task<IEnumerable<OperationVM>> GetAsync()
		{
			var models = await _unitOfWork.Operations.GetAllAsync(isTracking: false);

			if (models==null)
			{
				string errorMessage = $"{models!.GetType().Name} collection not found ";
				_logger.LogError(errorMessage);
				throw new ObjectNotFoundException(errorMessage);
			}

			else
			{
				var modelsVM = _mapper.Map<IEnumerable<OperationVM>>(models);
				return modelsVM;
			}
		}

		public async Task<OperationVM> GetByIdAsync(Guid id)
		{
			var models = await _unitOfWork.Operations
				.GetAllAsync(
					expression: _ => _.OperationId.Equals(id),
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
				var modelVM = _mapper.Map<OperationVM>(model);
				return modelVM;
			}
		}

		public async Task CreateAsync(OperationVM vm)
		{
			var model = _mapper.Map<Operation>(vm);
			_unitOfWork.Operations.Create(model);
			await _unitOfWork.SaveAsync();
		}

        public async Task UpdateAsync(OperationVM vm)
        {
            var model = _mapper.Map<Operation>(vm);
            var models = await _unitOfWork.Operations
                .GetAllAsync(
                    expression: _ => _.OperationId.Equals(model.OperationId),
                    isTracking: false);

            if (models==null)
            {
                string errorMessage = $"{model!.GetType().Name} model with id: {model.OperationId} not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            else
            {
                _unitOfWork.Operations.Update(model);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await _unitOfWork.Operations.GetOneAsync(
                expression: _ => _.OperationId.Equals(id),
                isTracking: false);

            if (model==null)
            {
                string errorMessage = $"{model!.GetType().Name} model with id: {id} not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            else
            {
                _unitOfWork.Operations.Delete(model);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
