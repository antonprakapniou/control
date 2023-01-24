using AutoMapper;
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
        private const string _typeName = "Period";

        private readonly ILogger<PeriodService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PeriodService(
            ILogger<PeriodService> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _logger=logger;
            _mapper=mapper;
            _unitOfWork=unitOfWork;
        }

        public async Task<IEnumerable<PeriodVM>> GetAllAsync()
        {
            var models = await _unitOfWork.Periods.GetAllByAsync();

            if (models==null)
            {
                string errorMessage = $"{_typeName} collection not found ";
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
            var model = await _unitOfWork.Periods.GetOneByAsync(_ => _.Id.Equals(id));

            if (model==null)
            {
                string errorMessage = $"{_typeName} collection not found ";
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
            if (int.TryParse(vm.Name, out int month))
            {
                var model = _mapper.Map<Period>(vm);
                model.Month= month;
                _unitOfWork.Periods.Create(model);
                await _unitOfWork.SaveAsync();
            }

            else throw new InvalidValueException("Invalid Period value");
            
        }

        public async Task UpdateAsync(PeriodVM vm)
        {
            if (_unitOfWork.Periods.IsExists(_ => _.Id.Equals(vm.Id)))
            {
                var model = _mapper.Map<Period>(vm);
                _unitOfWork.Periods.Update(model);
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
            var model = await _unitOfWork.Periods.GetOneByAsync(_ => _.Id.Equals(id));

            if (model==null)
            {
                string errorMessage = $"{_typeName} model with id: {id} not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            else
            {
                _unitOfWork.Periods.Delete(model);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
