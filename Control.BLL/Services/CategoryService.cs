namespace Control.BLL.Services;

public sealed class CategoryService : GenericService<CategoryVM, Category>, ICategoryService
{
    #region Own fields

    private readonly IMapper _mapper;
    private readonly IGenericRepository<Category> _repository;

    #endregion

    #region Ctor

    public CategoryService(
        IMapper mapper,
        IGenericRepository<Category> repository)
        : base(mapper, repository)
    {
        _mapper=mapper;
        _repository=repository;
    }

    #endregion

    #region Methods

    public override async Task<IEnumerable<CategoryVM>> GetAllAsync()
    {
        var models = await _repository.GetAllByAsync();

        if (models is null)
        {
            string errorMessage = $"'{models!.GetType().Name}' collection not found ";
            throw new ObjectNotFoundException(errorMessage);
        }

        var viewModels = _mapper.Map<IEnumerable<CategoryVM>>(models);
        var orderViewModels= viewModels.OrderBy(_ => _.Name);
        return orderViewModels;
    }

    #endregion
}