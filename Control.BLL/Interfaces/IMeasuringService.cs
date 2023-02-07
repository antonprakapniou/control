namespace Control.BLL.Interfaces;

public interface IMeasuringService : IGenericService<MeasuringVM, Measuring>
{
    #region Methods

    public Task<IEnumerable<SelectListItem>> GetSelectListAsync();

    #endregion
}