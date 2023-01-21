using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
	public sealed class UnitsService:IUnitsService
	{
		private readonly ILogger<MeasuringService> _logger;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public UnitsService(
			ILogger<MeasuringService> logger,
			IMapper mapper,
			IUnitOfWork unitOfWork)
		{
			_logger=logger;
			_mapper=mapper;
			_unitOfWork=unitOfWork;
		}

		public async Task<IEnumerable<UnitsVM>> GetAsync()
		{
			var models = await _unitOfWork.Units.GetAsync(isTracking: false);

			if (models==null)
			{
				string errorMessage = $"{models!.GetType().Name} collection not found ";
				_logger.LogError(errorMessage);
				throw new ObjectNotFoundException(errorMessage);
			}

			else
			{
				var modelsVM = _mapper.Map<IEnumerable<UnitsVM>>(models);
				return modelsVM;
			}
		}

		public async Task<UnitsVM> GetByIdAsync(Guid id)
		{
			var models = await _unitOfWork.Units
				.GetAsync(
					expression: _ => _.UnitsId.Equals(id),
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
				var modelVM = _mapper.Map<UnitsVM>(model);
				return modelVM;
			}
		}

		public async Task CreateAsync(UnitsVM vm)
		{
			var model = _mapper.Map<Units>(vm);
			_unitOfWork.Units.Create(model);
			await _unitOfWork.SaveAsync();
		}

		public async Task UpdateAsync(UnitsVM vm)
		{
			var id = vm.UnitsId;
			var models = await _unitOfWork.Units
				.GetAsync(
					expression: _ => _.UnitsId.Equals(id),
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
				_unitOfWork.Units.Update(model);
			}
		}

		public async Task DeleteAsync(UnitsVM vm)
		{
			var id = vm.UnitsId;
			var models = await _unitOfWork.Units
				.GetAsync(
					expression: _ => _.UnitsId.Equals(id),
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
				_unitOfWork.Units.Delete(model);
			}
		}
	}
}
