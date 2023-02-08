namespace Control.BLL.Interfaces;

public interface IPositionService : IGenericService<PositionVM, Position> 
{
    #region Methods

    public Task<IEnumerable<PositionVM>> GetAllByFilterAsync(FilterVM filter);
    public Task SetPositionSelectList(PositionCreatingVM viewModel);
    public Task SetFilterSelectList(FilterRepresentationVM viewModel);

    #endregion
}
