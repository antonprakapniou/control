﻿namespace Control.BLL.Services;

public sealed class OperationService : GenericService<OperationVM, Operation>, IOperationService
{
    #region Own fields

    private readonly ILogger<GenericService<OperationVM, Operation>> _logger;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Operation> _repository;

    #endregion

    #region Ctor

    public OperationService(
        ILogger<GenericService<OperationVM, Operation>> logger,
        IMapper mapper,
        IGenericRepository<Operation> repository)
        : base(logger, mapper, repository)
    {
        _logger=logger;
        _mapper=mapper;
        _repository=repository;
    }

    #endregion

    #region Methods

    public override async Task<IEnumerable<OperationVM>> GetAllAsync()
    {
        var models = await _repository.GetAllByAsync();

        if (models is null)
        {
            string errorMessage = $"'{models!.GetType().Name}' collection not found ";
            _logger.LogError(errorMessage);
            throw new ObjectNotFoundException(errorMessage);
        }

        var viewModels = _mapper.Map<IEnumerable<OperationVM>>(models);
        var orderViewModels = viewModels.OrderBy(_ => _.Name);

        return orderViewModels;
    }

    #endregion
}