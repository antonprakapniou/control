using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
    public class GenericService<V,T>:IGenericService<V,T>
        where V : BaseViewModel
        where T : BaseModel
    {
        private readonly ILogger<GenericService<V,T>> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<T> _repository;

        public GenericService(
            ILogger<GenericService<V,T>> logger,
            IMapper mapper,
            IGenericRepository<T> repository)
        {
            _logger=logger;
            _mapper=mapper;
            _repository=repository;
        }

        public virtual async Task<IEnumerable<V>> GetAllAsync()
        {
            var models = await _repository.GetAllByAsync();

            if (models is null)
            {
                string errorMessage = $"'{models!.GetType().Name}' collection not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            var viewModels = _mapper.Map<IEnumerable<V>>(models);
            return viewModels;
        }

        public virtual async Task<V> GetByIdAsync(Guid id)
        {
            var model = await _repository.GetOneByAsync(_=>_.Id.Equals(id));

            if (model is null)
            {
                string errorMessage = $"'{model!.GetType().Name}' with id: '{id}' not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            var viewModel = _mapper.Map<V>(model);
            return viewModel;
        }

        public virtual async Task CreateAsync(V viewModel)
        {
            var model = _mapper.Map<T>(viewModel);
            await _repository.CreateAsync(model);
        }

        public virtual async Task UpdateAsync(V viewModel)
        {
            var model = _mapper.Map<T>(viewModel);
            var modelFromDb= await _repository.GetOneByAsync(_ => _.Id.Equals(model.Id));

            if (modelFromDb is null)
            {
                string errorMessage = $"'{model!.GetType().Name}' with id: '{model.Id}' not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            await _repository.UpdateAsync(model);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var model = await _repository.GetOneByAsync(_ => _.Id.Equals(id));

            if (model is null)
            {
                string errorMessage = $"'{model!.GetType().Name}' with id: '{id}' not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            await _repository.DeleteAsync(model);
        }
    }
}
