﻿namespace Control.BLL.Services;
public sealed class PeriodService : GenericService<PeriodVM, Period>, IPeriodService
{
    #region Own fields

    private readonly IMapper _mapper;
    private readonly IGenericRepository<Period> _repository;

    #endregion

    #region Ctor

    public PeriodService(
        IMapper mapper,
        IGenericRepository<Period> repository)
        : base(mapper, repository)
    {
        _mapper=mapper;
        _repository=repository;
    }

    #endregion

    #region Methods

    public override async Task<IEnumerable<PeriodVM>> GetAllAsync()
    {
        var models = await _repository.GetAllByAsync();

        if (models is null)
        {
            string errorMessage = $"'{models!.GetType().Name}' collection not found ";
            throw new ObjectNotFoundException(errorMessage);
        }

        var viewModels = _mapper.Map<IEnumerable<PeriodVM>>(models);
        var orderViewModels = viewModels.OrderBy(_ => _.Month);

        return orderViewModels;
    }

    public override async Task CreateAsync(PeriodVM viewModel)
    {
        if (int.TryParse(viewModel.Name, out _))
        {
            var model = _mapper.Map<Period>(viewModel);
            await _repository.CreateAsync(model);
        }

        else throw new InvalidValueException("Invalid 'Period' value. It must be '012', for example");
    }

    #endregion
}