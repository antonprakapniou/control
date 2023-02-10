namespace Control.BLL.Services;

public sealed class OwnerService : GenericService<OwnerVM, Owner>, IOwnerService
{
    #region Own fields

    private readonly IMapper _mapper;
    private readonly IGenericRepository<Owner> _ownerRepository;
    private readonly IGenericRepository<Master> _masterRepository;

    #endregion

    #region Ctor

    public OwnerService(
        IMapper mapper,
        IGenericRepository<Owner> ownerRepository,
        IGenericRepository<Master> masterRepository)
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

        #region Include properties mapping

        foreach (var viewModel in viewModels)
        {
            if (viewModel.MasterId is not null)
            {
                var property = await _masterRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.MasterId));
                var propertyVM = _mapper.Map<MasterVM>(property);
                viewModel.Master = propertyVM;
            }
        }

        #endregion

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

        #region Include properties mapping

        if (viewModel.MasterId is not null)
        {
            var property = await _masterRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.MasterId));
            var propertyVM = _mapper.Map<MasterVM>(property);
            viewModel.Master = propertyVM;
        }

        #endregion

        return viewModel;
    }
    public async Task<OwnerCreatingVM> SetOwnerSelectList(OwnerCreatingVM viewModel)
    {
        var masters = await _masterRepository.GetAllByAsync();
        viewModel.Masters=masters.Select(_ => new SelectListItem { Value=_.Id.ToString(), Text=_.UserName });
        return viewModel;
    }

    #endregion
}
