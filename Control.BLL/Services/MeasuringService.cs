using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
	public class MeasuringService : IMeasuringService
	{
        private const string _typeName = "Measuring";

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

        public async Task<IEnumerable<MeasuringVM>> GetAllAsync()
        {
            var models = await _unitOfWork.Measurings.GetAllByAsync();

            if (models==null)
            {
                string errorMessage = $"{_typeName} collection not found ";
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
            var model = await _unitOfWork.Measurings.GetOneByAsync(_ => _.Id.Equals(id));

            if (model==null)
            {
                string errorMessage = $"{_typeName} collection not found ";
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
            var model = _mapper.Map<Measuring>(vm);
            _unitOfWork.Measurings.Create(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(MeasuringVM vm)
        {
            if (_unitOfWork.Measurings.IsExists(_ => _.Id.Equals(vm.Id)))
            {
                var model = _mapper.Map<Measuring>(vm);
                _unitOfWork.Measurings.Update(model);
                await _unitOfWork.SaveAsync();
            }

            else
            {
                string errorMessage = $"{_typeName} model with id: {vm.Id} not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await _unitOfWork.Measurings.GetOneByAsync(_ => _.Id.Equals(id));

            if (model==null)
            {
                string errorMessage = $"{_typeName} model with id: {id} not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            else
            {
                _unitOfWork.Measurings.Delete(model);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
