namespace Control.BLL.Interfaces;

public interface IPositionService : IGenericService<PositionVM, Position> 
{
    #region Methods

    public Task<IEnumerable<PositionVM>> GetAllByFilterAsync(FilterVM filter);

    #endregion
}
