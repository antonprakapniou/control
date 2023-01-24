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
        private const string _typeName = "Nomination";

        private readonly ILogger<NominationService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public NominationService(
            ILogger<NominationService> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _logger=logger;
            _mapper=mapper;
            _unitOfWork=unitOfWork;
        }

        public async Task<IEnumerable<NominationVM>> GetAllAsync()
        {
            var models = await _unitOfWork.Nominations.GetAllByAsync();

            if (models==null)
            {
                string errorMessage = $"{_typeName} collection not found ";
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
            var model = await _unitOfWork.Nominations.GetOneByAsync(_ => _.Id.Equals(id));

            if (model==null)
            {
                string errorMessage = $"{_typeName} collection not found ";
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
            if (_unitOfWork.Nominations.IsExists(_ => _.Id.Equals(vm.Id)))
            {
                var model = _mapper.Map<Nomination>(vm);
                _unitOfWork.Nominations.Update(model);
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
            var model = await _unitOfWork.Nominations.GetOneByAsync(_ => _.Id.Equals(id));

            if (model==null)
            {
                string errorMessage = $"{_typeName} model with id: {id} not found ";
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
