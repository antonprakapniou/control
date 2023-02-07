namespace Control.BLL.Interfaces;

public interface INominationService : IGenericService<NominationVM, Nomination>
{
    #region Methods

    public Task<IEnumerable<SelectListItem>> GetSelectListAsync();

    #endregion
}
