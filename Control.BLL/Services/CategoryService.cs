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

        public async Task<IEnumerable<CategoryVM>> GetAsync()
        {
            var models = await _unitOfWork.Categories.GetAllAsync(isTracking: false);

            if (models==null)
            {
                string errorMessage = $"{models!.GetType().Name} collection not found ";
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
            var models = await _unitOfWork.Categories
                .GetAllAsync(
                    expression: _ => _.CategoryId.Equals(id),
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
            var model = _mapper.Map<Category>(vm);
            var models = await _unitOfWork.Categories
                .GetAllAsync(
                    expression: _ => _.CategoryId.Equals(model.CategoryId),
                    isTracking: false);

            if (models==null)
            {
                string errorMessage = $"{model!.GetType().Name} model with id: {model.CategoryId} not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            else
            {
                _unitOfWork.Categories.Update(model);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await _unitOfWork.Categories.GetOneAsync(
                expression:_=>_.CategoryId.Equals(id),
                isTracking:false);

            if (model==null)
            {
                string errorMessage = $"{model!.GetType().Name} model with id: {id} not found ";
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
