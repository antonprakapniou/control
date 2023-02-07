namespace Control.BLL.Interfaces; 

public interface IPeriodService : IGenericService<PeriodVM, Period>
{
    #region Methods

    public Task<IEnumerable<SelectListItem>> GetSelectListAsync();

    #endregion
}
