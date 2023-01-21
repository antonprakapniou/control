using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
	public class MeasuringService:IMeasuringService
	{
		private readonly ILogger<MeasuringService> _logger;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public MeasuringService(
			ILogger<MeasuringService> logger,
			IMapper mapper,
			IUnitOfWork unitOfWork)
		{
			_logger=logger;
			_mapper=mapper;
			_unitOfWork=unitOfWork;
		}

		public async Task<IEnumerable<MeasuringVM>> GetAsync()
		{
			var models = await _unitOfWork.Measurings.GetAsync(isTracking:false);

			if (models==null)
			{
				string errorMessage = $"{models!.GetType().Name} collection not found ";
				_logger.LogError(errorMessage);
				throw new ObjectNotFoundException(errorMessage);
			}

			else
			{
				var modelsVM = _mapper.Map<IEnumerable<MeasuringVM>>(models);
				return modelsVM;
			}
		}

		public async Task<MeasuringVM> GetByIdAsync(Guid id)
		{
			var models = await _unitOfWork.Measurings
				.GetAsync(
					expression: _ => _.MeasuringId.Equals(id),
					isTracking: false);

			var model=models.FirstOrDefault();

			if (model==null)
			{
				string errorMessage = $"{model!.GetType().Name} model with id: {id} not found ";
				_logger.LogError(errorMessage);
				throw new ObjectNotFoundException(errorMessage);
			}

			else
			{
				var modelVM = _mapper.Map<MeasuringVM>(model);
				return modelVM;
			}
		}

		public async Task CreateAsync(MeasuringVM vm)
		{
			var model=_mapper.Map<Measuring>(vm);
			_unitOfWork.Measurings.Create(model);
			await _unitOfWork.SaveAsync();
		}

		public async Task UpdateAsync(MeasuringVM vm)
		{
			var id=vm.MeasuringId;
			var models = await _unitOfWork.Measurings
				.GetAsync(
					expression: _ => _.MeasuringId.Equals(id),
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
				_unitOfWork.Measurings.Update(model);
			}
		}

		public async Task DeleteAsync(MeasuringVM vm)
		{
			var id = vm.MeasuringId;
			var models = await _unitOfWork.Measurings
				.GetAsync(
					expression: _ => _.MeasuringId.Equals(id),
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
				_unitOfWork.Measurings.Delete(model);
			}
		}
	}
}
