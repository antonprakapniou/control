using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
    public sealed class CategoryService : GenericService<CategoryVM, Category>,ICategoryService
    {
        private readonly ILogger<GenericService<CategoryVM, Category>> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Category> _repository;

        public CategoryService(
            ILogger<GenericService<CategoryVM, Category>> logger,
            IMapper mapper,
            IGenericRepository<Category> repository)
            : base(logger, mapper, repository) 
        {
            _logger=logger;
            _mapper=mapper;
            _repository=repository;
        }

        public override async Task<IEnumerable<CategoryVM>> GetAllAsync()
        {
            var models = await _repository.GetAllByAsync();

            if (models is null)
            {
                string errorMessage = $"'{models!.GetType().Name}' collection not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            var orderModels=models.OrderBy(_ => _.Name);
            var viewModels = _mapper.Map<IEnumerable<CategoryVM>>(orderModels);
            return viewModels;
        }
    }
}
