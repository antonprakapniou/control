using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
	public sealed class OwnerService:IOwnerService
	{
        private const string _typeName = "Owner";

        private readonly ILogger<OwnerService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OwnerService(
            ILogger<OwnerService> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _logger=logger;
            _mapper=mapper;
            _unitOfWork=unitOfWork;
        }

        public async Task<IEnumerable<OwnerVM>> GetAllAsync()
        {
            var models = await _unitOfWork.Owners.GetAllByAsync();

            if (models==null)
            {
                string errorMessage = $"{_typeName} collection not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            else
            {
                var modelsVM = _mapper.Map<IEnumerable<OwnerVM>>(models);
                return modelsVM;
            }
        }

        public async Task<OwnerVM> GetByIdAsync(Guid id)
        {
            var model = await _unitOfWork.Owners.GetOneByAsync(_ => _.Id.Equals(id));

            if (model==null)
            {
                string errorMessage = $"{_typeName} collection not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            else
            {
                var modelVM = _mapper.Map<OwnerVM>(model);
                return modelVM;
            }
        }

        public async Task CreateAsync(OwnerVM vm)
        {
            var model = _mapper.Map<Owner>(vm);
            _unitOfWork.Owners.Create(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(OwnerVM vm)
        {
            if (_unitOfWork.Owners.IsExists(_ => _.Id.Equals(vm.Id)))
            {
                var model = _mapper.Map<Owner>(vm);
                _unitOfWork.Owners.Update(model);
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
            var model = await _unitOfWork.Owners.GetOneByAsync(_ => _.Id.Equals(id));

            if (model==null)
            {
                string errorMessage = $"{_typeName} model with id: {id} not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            else
            {
                _unitOfWork.Owners.Delete(model);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
