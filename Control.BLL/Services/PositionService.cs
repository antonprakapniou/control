namespace Control.BLL.Services;
public sealed class PositionService : GenericService<PositionVM, Position>, IPositionService
{
    #region Own fields

    private readonly ILogger<GenericService<PositionVM, Position>> _logger;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Position> _positionRepository;
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IGenericRepository<Measuring> _measuringRepository;
    private readonly IGenericRepository<Nomination> _nominationRepository;
    private readonly IGenericRepository<Operation> _operationRepository;
    private readonly IGenericRepository<Owner> _ownerRepository;
    private readonly IGenericRepository<Period> _periodRepository;

    #endregion

    #region Ctor

    public PositionService(
        ILogger<GenericService<PositionVM, Position>> logger,
        IMapper mapper,
        IGenericRepository<Position> positionRepository,
        IGenericRepository<Period> periodRepository,
        IGenericRepository<Category> categoryRepository,
        IGenericRepository<Measuring> measuringRepository,
        IGenericRepository<Nomination> nominationRepository,
        IGenericRepository<Operation> operationRepository,
        IGenericRepository<Owner> ownerRepository)
        : base(logger, mapper, positionRepository)
    {
        _logger=logger;
        _mapper = mapper;
        _positionRepository = positionRepository;
        _periodRepository = periodRepository;
        _categoryRepository = categoryRepository;
        _measuringRepository = measuringRepository;
        _nominationRepository = nominationRepository;
        _operationRepository = operationRepository;
        _ownerRepository = ownerRepository;
    }

    #endregion

    #region Methods

    public override async Task<IEnumerable<PositionVM>> GetAllAsync()
    {
        var models = await _positionRepository.GetAllByAsync();

        if (models is null)
        {
            string errorMessage = $"'{models!.GetType().Name}' collection not found ";
            _logger.LogError(errorMessage);
            throw new ObjectNotFoundException(errorMessage);
        }

        var viewModels = _mapper.Map<IEnumerable<PositionVM>>(models);

        #region Include properties mapping

        foreach (var viewModel in viewModels)
        {
            if (viewModel.CategoryId is not null)
            {
                var property = await _categoryRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.CategoryId));
                var propertyVM = _mapper.Map<CategoryVM>(property);
                viewModel.Category = propertyVM;
            }

            if (viewModel.MeasuringId is not null)
            {
                var property = await _measuringRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.MeasuringId));
                var propertyVM = _mapper.Map<MeasuringVM>(property);
                viewModel.Measuring = propertyVM;
            }

            if (viewModel.NominationId is not null)
            {
                var property = await _nominationRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.NominationId));
                var propertyVM = _mapper.Map<NominationVM>(property);
                viewModel.Nomination = propertyVM;
            }

            if (viewModel.OperationId is not null)
            {
                var property = await _operationRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.OperationId));
                var propertyVM = _mapper.Map<OperationVM>(property);
                viewModel.Operation = propertyVM;
            }

            if (viewModel.OwnerId is not null)
            {
                var property = await _ownerRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.OwnerId));
                var propertyVM = _mapper.Map<OwnerVM>(property);
                viewModel.Owner = propertyVM;
            }

            if (viewModel.PeriodId is not null)
            {
                var property = await _periodRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.PeriodId));
                var propertyVM = _mapper.Map<PeriodVM>(property);
                viewModel.Period = propertyVM;
            }
        }

        #endregion

        var orderViewModels = viewModels
            .OrderBy(_ => _.Measuring?.Code)
            .ThenBy(_ => _.Nomination?.Name)
            .ThenBy(_ => _.Owner?.ShopCode)
            .ThenBy(_ => _.DeviceType)
            .ThenBy(_ => _.FactoryNumber);

        return orderViewModels;
    }
    public override async Task<PositionVM> GetByIdAsync(Guid id)
    {
        var model = await _positionRepository.GetOneByAsync(_ => _.Id.Equals(id));

        if (model is null)
        {
            string errorMessage = $"'{model!.GetType().Name}' with id: '{id}' not found ";
            _logger.LogError(errorMessage);
            throw new ObjectNotFoundException(errorMessage);
        }

        var viewModel = _mapper.Map<PositionVM>(model);
        viewModel.AdviceDate=viewModel.NextDate;

        #region Include properties mapping

        if (viewModel.CategoryId is not null)
        {
            var property = await _categoryRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.CategoryId));
            var propertyVM = _mapper.Map<CategoryVM>(property);
            viewModel.Category = propertyVM;
        }

        if (viewModel.MeasuringId is not null)
        {
            var property = await _measuringRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.MeasuringId));
            var propertyVM = _mapper.Map<MeasuringVM>(property);
            viewModel.Measuring = propertyVM;
        }

        if (viewModel.NominationId is not null)
        {
            var property = await _nominationRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.NominationId));
            var propertyVM = _mapper.Map<NominationVM>(property);
            viewModel.Nomination = propertyVM;
        }

        if (viewModel.OperationId is not null)
        {
            var property = await _operationRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.OperationId));
            var propertyVM = _mapper.Map<OperationVM>(property);
            viewModel.Operation = propertyVM;
        }

        if (viewModel.OwnerId is not null)
        {
            var property = await _ownerRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.OwnerId));
            var propertyVM = _mapper.Map<OwnerVM>(property);
            viewModel.Owner = propertyVM;
        }

        if (viewModel.PeriodId is not null)
        {
            var property = await _periodRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.PeriodId));
            var propertyVM = _mapper.Map<PeriodVM>(property);
            viewModel.Period = propertyVM;
        }

        #endregion

        return viewModel;
    }
    public override async Task CreateAsync(PositionVM vm)
    {
        vm.NextDate=(vm.AdviceDate<vm.PreviousDate) ? vm.NeedDate : vm.AdviceDate;
        vm.Created= DateTime.Now;
        var model = _mapper.Map<Position>(vm);
        await _positionRepository.CreateAsync(model);
    }
    public override async Task UpdateAsync(PositionVM vm)
    {
        vm.NextDate=(vm.AdviceDate<vm.PreviousDate) ? vm.NeedDate : vm.AdviceDate;
        var model = _mapper.Map<Position>(vm);
        var modelFromDb = await _positionRepository.GetOneByAsync(_ => _.Id.Equals(model.Id));

        if (modelFromDb is null)
        {
            string errorMessage = $"'{model!.GetType().Name}' with id: '{model.Id}' not found ";
            _logger.LogError(errorMessage);
            throw new ObjectNotFoundException(errorMessage);
        }

        var modelFromDbCreated = modelFromDb.Created;
        model.Created=modelFromDbCreated;
        await _positionRepository.UpdateAsync(model);
    }

    #endregion
}
