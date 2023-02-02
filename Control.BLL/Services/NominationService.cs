namespace Control.BLL.Services;

public sealed class NominationService : GenericService<NominationVM, Nomination>, INominationService
{
    #region Own fields

    private readonly ILogger<GenericService<NominationVM, Nomination>> _logger;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Nomination> _repository;

    #endregion

    #region Ctor

    public NominationService(
       ILogger<GenericService<NominationVM, Nomination>> logger,
       IMapper mapper,
       IGenericRepository<Nomination> repository)
       : base(logger, mapper, repository)
    {
        _logger=logger;
        _mapper=mapper;
        _repository=repository;
    }

    #endregion

    #region Methods

    public override async Task<IEnumerable<NominationVM>> GetAllAsync()
    {
        var models = await _repository.GetAllByAsync();

        if (models is null)
        {
            string errorMessage = $"'{models!.GetType().Name}' collection not found ";
            _logger.LogError(errorMessage);
            throw new ObjectNotFoundException(errorMessage);
        }

        var viewModels = _mapper.Map<IEnumerable<NominationVM>>(models);
        var orderViewModels = viewModels.OrderBy(_ => _.Name);

        return orderViewModels;
    }

    #endregion
}