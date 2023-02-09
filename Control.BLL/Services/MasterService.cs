namespace Control.BLL.Services;

public sealed class MasterService : IMasterService
{
    #region Own fields

    private readonly IMapper _mapper;
    private readonly IMasterRepository _repository;

    #endregion

    #region Ctor

    public MasterService
        (IMapper mapper,
        IMasterRepository repository)
    {
        _mapper= mapper;
        _repository= repository;
    }

    #endregion

    #region Methods    

    public async Task<IEnumerable<IdentityMasterVM>> GetAllAsync()
    {
        var models = await _repository.GetAllByAsync();

        if (models is null)
        {
            string errorMessage = $"'{models!.GetType().Name}' collection not found ";
            throw new ObjectNotFoundException(errorMessage);
        }

        var viewModels = _mapper.Map<IEnumerable<IdentityMasterVM>>(models);
        return viewModels;
    }
    public async Task<IdentityMasterVM> GetByIdAsync(Guid id)
    {
        var model = await _repository.GetOneByAsync(_ => _.Id.Equals(id));

        if (model is null)
        {
            string errorMessage = $"'{model!.GetType().Name}' with id: '{id}' not found ";
            throw new ObjectNotFoundException(errorMessage);
        }

        var viewModel = _mapper.Map<IdentityMasterVM>(model);
        return viewModel;
    }
    public async Task DeleteAsync(Guid id)
    {
        var model = await _repository.GetOneByAsync(_ => _.Id.Equals(id));

        if (model is null)
        {
            string errorMessage = $"'{model!.GetType().Name}' with id: '{id}' not found ";
            throw new ObjectNotFoundException(errorMessage);
        }

        await _repository.DeleteAsync(model);
    }

    #endregion
}
