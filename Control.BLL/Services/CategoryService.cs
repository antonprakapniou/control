using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
    public sealed class CategoryService:ICategoryService
    {
        private const string _typeName = "Category";

        private readonly ILogger<CategoryService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(
            ILogger<CategoryService> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _logger=logger;
            _mapper=mapper;
            _unitOfWork=unitOfWork;
        }

        public async Task<IEnumerable<CategoryVM>> GetAllAsync()
        {
            var models = await _unitOfWork.Categories.GetAllByAsync();

            if (models==null)
            {
                string errorMessage = $"{_typeName} collection not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            else
            {
                var modelsVM = _mapper.Map<IEnumerable<CategoryVM>>(models);
                return modelsVM;
            }
        }

        public async Task<CategoryVM> GetByIdAsync(Guid id)
        {
            var model = await _unitOfWork.Categories.GetOneByAsync(_=>_.Id.Equals(id));

            if (model==null)
            {
                string errorMessage = $"{_typeName} collection not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            else
            {
                var modelVM = _mapper.Map<CategoryVM>(model);
                return modelVM;
            }
        }

        public async Task CreateAsync(CategoryVM vm)
        {
            var model = _mapper.Map<Category>(vm);
            _unitOfWork.Categories.Create(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(CategoryVM vm)
        {
            if (_unitOfWork.Categories.IsExists(_ => _.Id.Equals(vm.Id)))
            {
                var model = _mapper.Map<Category>(vm);
                _unitOfWork.Categories.Update(model);
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
            var model = await _unitOfWork.Categories.GetOneByAsync(_=>_.Id.Equals(id));

            if (model==null)
            {
                string errorMessage = $"{_typeName} model with id: {id} not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            else
            {
                _unitOfWork.Categories.Delete(model);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
