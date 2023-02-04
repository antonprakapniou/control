namespace Control.BLL.Services;

public sealed class MasterService:GenericService<MasterVM, Master>,IMasterService
{
    #region Own fields

    private readonly IMapper _mapper;
    private readonly IGenericRepository<Master> _repository;

    #endregion

    #region Ctor

    public MasterService(
        IMapper mapper,
        IGenericRepository<Master> repository)
        : base(mapper, repository)
    {
        _mapper=mapper;
        _repository=repository;
    }

    #endregion

    #region Methods

    public override async Task<IEnumerable<MasterVM>> GetAllAsync()
    {
        var models = await _repository.GetAllByAsync();

        if (models is null)
        {
            string errorMessage = $"'{models!.GetType().Name}' collection not found ";
            throw new ObjectNotFoundException(errorMessage);
        }

        var viewModels = _mapper.Map<IEnumerable<MasterVM>>(models);
        var orderViewModels = viewModels.OrderBy(_ => _.Name);
        return orderViewModels;
    }

    #endregion
}
