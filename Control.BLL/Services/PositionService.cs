namespace Control.BLL.Services;
public sealed class PositionService : GenericService<PositionVM, Position>, IPositionService
{
    #region Own fields

    private readonly IMapper _mapper;
    private readonly IGenericRepository<Position> _positionRepository;
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IGenericRepository<Measuring> _measuringRepository;
    private readonly IGenericRepository<Master> _masterRepository;
    private readonly IGenericRepository<Nomination> _nominationRepository;
    private readonly IGenericRepository<Operation> _operationRepository;
    private readonly IGenericRepository<Owner> _ownerRepository;
    private readonly IGenericRepository<Period> _periodRepository;

    #endregion

    #region Ctor

    public PositionService(
        IMapper mapper,
        IGenericRepository<Position> positionRepository,
        IGenericRepository<Period> periodRepository,
        IGenericRepository<Category> categoryRepository,
        IGenericRepository<Measuring> measuringRepository,
        IGenericRepository<Master> masterRepository,
        IGenericRepository<Nomination> nominationRepository,
        IGenericRepository<Operation> operationRepository,
        IGenericRepository<Owner> ownerRepository)
        : base(mapper, positionRepository)
    {
        _mapper = mapper;
        _positionRepository = positionRepository;
        _periodRepository = periodRepository;
        _categoryRepository = categoryRepository;
        _measuringRepository = measuringRepository;
        _masterRepository = masterRepository;
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
            throw new ObjectNotFoundException(errorMessage);
        }

        var viewModels = _mapper.Map<IEnumerable<PositionVM>>(models);

        #region Include properties mapping

        foreach (var viewModel in viewModels)
        {
            await SetIncludePropertiesAsync(viewModel);
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
            throw new ObjectNotFoundException(errorMessage);
        }

        var viewModel = _mapper.Map<PositionVM>(model);
        viewModel.AdviceDate=viewModel.NextDate;

        #region Include properties mapping

        await SetIncludePropertiesAsync(viewModel);

        #endregion

        return viewModel;
    }
    public override async Task CreateAsync(PositionVM vm)
    {
        if (vm.PeriodId is not null)
        {
            var period = await _periodRepository.GetOneByAsync(_ => _.Id.Equals(vm.PeriodId));
            var periodVM = _mapper.Map<PeriodVM>(period);
            vm.NeedDate=vm.PreviousDate.AddMonths(periodVM.Month).AddDays(-1);
        }

        else vm.NeedDate=vm.PreviousDate;

        vm.NextDate=(vm.AdviceDate<vm.PreviousDate) ? vm.NeedDate : vm.AdviceDate;
        vm.Created= DateTime.Now;
        var model = _mapper.Map<Position>(vm);
        await _positionRepository.CreateAsync(model);
    }
    public override async Task UpdateAsync(PositionVM vm)
    {
        if (vm.PeriodId is not null)
        {
            var period = await _periodRepository.GetOneByAsync(_ => _.Id.Equals(vm.PeriodId));
            var periodVM = _mapper.Map<PeriodVM>(period);
            vm.NeedDate=vm.PreviousDate.AddMonths(periodVM.Month).AddDays(-1);
        }

        vm.NextDate=(vm.AdviceDate<vm.PreviousDate) ? vm.NeedDate : vm.AdviceDate;
        var model = _mapper.Map<Position>(vm);
        var modelFromDb = await _positionRepository.GetOneByAsync(_ => _.Id.Equals(model.Id));

        if (modelFromDb is null)
        {
            string errorMessage = $"'{model!.GetType().Name}' with id: '{model.Id}' not found ";
            throw new ObjectNotFoundException(errorMessage);
        }

        var modelFromDbCreated = modelFromDb.Created;
        model.Created=modelFromDbCreated;
        await _positionRepository.UpdateAsync(model);
    }
    public async Task<IEnumerable<PositionVM>> GetAllByFilterAsync(FilterVM filter)
    {
        var expressions = new List<Expression<Func<Position, bool>>>();
        if (filter.CategoryId is not null) expressions.Add(_ => _.CategoryId.Equals(filter.CategoryId));
        if (filter.MeasuringId is not null) expressions.Add(_ => _.MeasuringId.Equals(filter.MeasuringId));
        if (filter.NominationId is not null) expressions.Add(_ => _.NominationId.Equals(filter.NominationId));
        if (filter.OperationId is not null) expressions.Add(_ => _.OperationId.Equals(filter.OperationId));
        if (filter.OwnerId is not null) expressions.Add(_ => _.OwnerId.Equals(filter.OwnerId));
        if (filter.PeriodId is not null) expressions.Add(_ => _.PeriodId.Equals(filter.PeriodId));
        if (filter.Month is not null) expressions.Add(_ => _.NextDate.Year.Equals(DateTime.Now.Year)&&_.NextDate.Month.Equals(filter.Month));
        if (filter.Number is not null) expressions.Add(_ => _.FactoryNumber!.ToUpper().Contains(filter.Number.ToUpper())||filter.Number.ToUpper().Contains(_.FactoryNumber.ToUpper()));
        var models = await _positionRepository.GetAllByFilterAsync(expressions.ToArray());
        var viewModels = _mapper.Map<IEnumerable<PositionVM>>(models);

        #region Include properties mapping

        foreach (var viewModel in viewModels)
        {
            await SetIncludePropertiesAsync(viewModel);
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
    public async Task SetPositionSelectList(PositionCreatingVM viewModel)
    {
        var categories = await _categoryRepository.GetAllByAsync();
        var measurings = await _measuringRepository.GetAllByAsync();
        var nominations = await _nominationRepository.GetAllByAsync();
        var operations = await _operationRepository.GetAllByAsync();
        var owners = await _ownerRepository.GetAllByAsync();
        var periods = await _periodRepository.GetAllByAsync();

        viewModel.Categories=categories.Select(_ => new SelectListItem { Value=_.Id.ToString(), Text=_.Name });
        viewModel.Measurings=measurings.Select(_ => new SelectListItem { Value=_.Id.ToString(), Text=_.Code });
        viewModel.Nominations=nominations.Select(_ => new SelectListItem { Value=_.Id.ToString(), Text=_.Name });
        viewModel.Operations=operations.Select(_ => new SelectListItem { Value=_.Id.ToString(), Text=_.Name });
        viewModel.Owners=owners.Select(_ => new SelectListItem { Value=_.Id.ToString(), Text=$"{_.ShortShop} {_.ShortProduction}" });
        viewModel.Periods=periods.Select(_ => new SelectListItem { Value=_.Id.ToString(), Text=_.Name });
    }
    public async Task SetFilterSelectList(FilterRepresentationVM viewModel)
    {
        var categories = await _categoryRepository.GetAllByAsync();
        var measurings = await _measuringRepository.GetAllByAsync();
        var nominations = await _nominationRepository.GetAllByAsync();
        var operations = await _operationRepository.GetAllByAsync();
        var owners = await _ownerRepository.GetAllByAsync();
        var periods = await _periodRepository.GetAllByAsync();

        viewModel.Categories=categories.Select(_ => new SelectListItem { Value=_.Id.ToString(), Text=_.Name });
        viewModel.Measurings=measurings.Select(_ => new SelectListItem { Value=_.Id.ToString(), Text=_.Code });
        viewModel.Nominations=nominations.Select(_ => new SelectListItem { Value=_.Id.ToString(), Text=_.Name });
        viewModel.Operations=operations.Select(_ => new SelectListItem { Value=_.Id.ToString(), Text=_.Name });
        viewModel.Owners=owners.Select(_ => new SelectListItem { Value=_.Id.ToString(), Text=$"{_.ShortShop} {_.ShortProduction}" });
        viewModel.Periods=periods.Select(_ => new SelectListItem { Value=_.Id.ToString(), Text=_.Name });
    }
    private async Task SetIncludePropertiesAsync(PositionVM viewModel)
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

            if (propertyVM.MasterId is not null)
            {
                var master = await _masterRepository.GetOneByAsync(_ => _.Id.Equals(propertyVM.MasterId));
                var masterVM = _mapper.Map<MasterVM>(master);
                propertyVM.Master=masterVM;
            }

            viewModel.Owner = propertyVM;
        }
        if (viewModel.PeriodId is not null)
        {
            var property = await _periodRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.PeriodId));
            var propertyVM = _mapper.Map<PeriodVM>(property);
            viewModel.Period = propertyVM;
            viewModel.NeedDate=viewModel.PreviousDate.AddMonths(propertyVM.Month).AddDays(-1);
        }
    }

    #endregion
}
