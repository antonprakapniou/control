using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
	public sealed class NominationService:INominationService
	{
		private readonly ILogger<MeasuringService> _logger;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public NominationService(
			ILogger<MeasuringService> logger,
			IMapper mapper,
			IUnitOfWork unitOfWork)
		{
			_logger=logger;
			_mapper=mapper;
			_unitOfWork=unitOfWork;
		}

		public async Task<IEnumerable<NominationVM>> GetAsync()
		{
			var models = await _unitOfWork.Nominations.GetAsync(isTracking: false);

			if (models==null)
			{
				string errorMessage = $"{models!.GetType().Name} collection not found ";
				_logger.LogError(errorMessage);
				throw new ObjectNotFoundException(errorMessage);
			}

			else
			{
				var modelsVM = _mapper.Map<IEnumerable<NominationVM>>(models);
				return modelsVM;
			}
		}

		public async Task<NominationVM> GetByIdAsync(Guid id)
		{
			var models = await _unitOfWork.Nominations
				.GetAsync(
					expression: _ => _.NominationId.Equals(id),
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
				var modelVM = _mapper.Map<NominationVM>(model);
				return modelVM;
			}
		}

		public async Task CreateAsync(NominationVM vm)
		{
			var model = _mapper.Map<Nomination>(vm);
			_unitOfWork.Nominations.Create(model);
			await _unitOfWork.SaveAsync();
		}

        public async Task UpdateAsync(NominationVM vm)
        {
            var model = _mapper.Map<Nomination>(vm);
            var models = await _unitOfWork.Nominations
                .GetAsync(
                    expression: _ => _.NominationId.Equals(model.NominationId),
                    isTracking: false);

            if (models==null)
            {
                string errorMessage = $"{model!.GetType().Name} model with id: {model.NominationId} not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            else
            {
                _unitOfWork.Nominations.Update(model);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task DeleteAsync(NominationVM vm)
        {
            var model = _mapper.Map<Nomination>(vm);
            var models = await _unitOfWork.Nominations
                .GetAsync(
                    expression: _ => _.NominationId.Equals(model.NominationId),
                    isTracking: false);

            if (models==null)
            {
                string errorMessage = $"{model!.GetType().Name} model with id: {model.NominationId} not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            else
            {
                _unitOfWork.Nominations.Delete(model);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
