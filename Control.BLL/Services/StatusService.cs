using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
	public sealed class StatusService:IStatusService
	{
		private readonly ILogger<MeasuringService> _logger;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public StatusService(
			ILogger<MeasuringService> logger,
			IMapper mapper,
			IUnitOfWork unitOfWork)
		{
			_logger=logger;
			_mapper=mapper;
			_unitOfWork=unitOfWork;
		}

		public async Task<IEnumerable<StatusVM>> GetAsync()
		{
			var models = await _unitOfWork.Statuses.GetAsync(isTracking: false);

			if (models==null)
			{
				string errorMessage = $"{models!.GetType().Name} collection not found ";
				_logger.LogError(errorMessage);
				throw new ObjectNotFoundException(errorMessage);
			}

			else
			{
				var modelsVM = _mapper.Map<IEnumerable<StatusVM>>(models);
				return modelsVM;
			}
		}

		public async Task<StatusVM> GetByIdAsync(Guid id)
		{
			var models = await _unitOfWork.Statuses
				.GetAsync(
					expression: _ => _.StatusId.Equals(id),
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
				var modelVM = _mapper.Map<StatusVM>(model);
				return modelVM;
			}
		}

		public async Task CreateAsync(StatusVM vm)
		{
			var model = _mapper.Map<Status>(vm);
			_unitOfWork.Statuses.Create(model);
			await _unitOfWork.SaveAsync();
		}

		public async Task UpdateAsync(StatusVM vm)
		{
			var id = vm.StatusId;
			var models = await _unitOfWork.Statuses
				.GetAsync(
					expression: _ => _.StatusId.Equals(id),
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
				_unitOfWork.Statuses.Update(model);
			}
		}

		public async Task DeleteAsync(StatusVM vm)
		{
			var id = vm.StatusId;
			var models = await _unitOfWork.Statuses
				.GetAsync(
					expression: _ => _.StatusId.Equals(id),
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
				_unitOfWork.Statuses.Delete(model);
			}
		}
	}
}
