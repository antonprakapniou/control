namespace Control.BLL.Services;

public sealed class OwnerService : GenericService<OwnerVM, Owner>, IOwnerService
{
    #region Own fields

    private readonly IMapper _mapper;
    private readonly IGenericRepository<Owner> _ownerRepository;
    private readonly IMasterRepository _masterRepository;

    #endregion

    #region Ctor

    public OwnerService(
        IMapper mapper,
        IGenericRepository<Owner> ownerRepository,
        IMasterRepository masterRepository)
        : base(mapper, ownerRepository)
    {
        _mapper=mapper;
        _ownerRepository=ownerRepository;
        _masterRepository=masterRepository;
    }

    #endregion

    #region Methods

    public override async Task<IEnumerable<OwnerVM>> GetAllAsync()
    {
        var models = await _ownerRepository.GetAllByAsync();

        if (models is null)
        {
            string errorMessage = $"'{models!.GetType().Name}' collection not found ";
            throw new ObjectNotFoundException(errorMessage);
        }

        var viewModels = _mapper.Map<IEnumerable<OwnerVM>>(models);

        foreach (var viewModel in viewModels) await SetIncludePropertiesAsync(viewModel);

        var orderViewModels = viewModels.OrderBy(_ => _.ShortName);

        return orderViewModels;
    }    
    public override async Task<OwnerVM> GetByIdAsync(Guid id)
    {
        var model = await _ownerRepository.GetOneByAsync(_ => _.Id.Equals(id));

        if (model is null)
        {
            string errorMessage = $"'{model!.GetType().Name}' with id: '{id}' not found ";
            throw new ObjectNotFoundException(errorMessage);
        }

        var viewModel = _mapper.Map<OwnerVM>(model);

        await SetIncludePropertiesAsync(viewModel);

        return viewModel;
    }
    public async Task<OwnerCreatingVM> SetOwnerSelectList(OwnerCreatingVM viewModel)
    {
        var masters = await _masterRepository.GetAllByAsync();
        viewModel.Masters=masters.Select(_ => new SelectListItem { Value=_.Id.ToString(), Text=_.Name });
        return viewModel;
    }
    private async Task SetIncludePropertiesAsync(OwnerVM viewModel)
    {
        if (viewModel.MasterId is not null)
        {
            var property = await _masterRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.MasterId));
            var propertyVM = _mapper.Map<MasterVM>(property);
            viewModel.Master = propertyVM;
        }        
    }

    #endregion
}
