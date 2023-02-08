namespace Control.BLL.Services;

public class MeasuringService : GenericService<MeasuringVM, Measuring>, IMeasuringService
{
    #region Own fields

    private readonly ILogger<GenericService<MeasuringVM, Measuring>> _logger;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Measuring> _repository;

    #endregion

    #region Ctor

    public MeasuringService(
        ILogger<GenericService<MeasuringVM, Measuring>> logger,
        IMapper mapper,
        IGenericRepository<Measuring> repository)
        : base(mapper, repository)
    {
        _logger=logger;
        _mapper=mapper;
        _repository=repository;
    }

    #endregion

    #region Methods

    public override async Task<IEnumerable<MeasuringVM>> GetAllAsync()
    {
        var models = await _repository.GetAllByAsync();

        if (models is null)
        {
            string errorMessage = $"'{models!.GetType().Name}' collection not found ";
            throw new ObjectNotFoundException(errorMessage);
        }

        var viewModels = _mapper.Map<IEnumerable<MeasuringVM>>(models);
        var orderViewModels = viewModels.OrderBy(_ => _.Code);

        return orderViewModels;
    }

    #endregion
}