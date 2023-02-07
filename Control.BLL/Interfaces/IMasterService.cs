namespace Control.BLL.Interfaces;

public interface IMasterService : IGenericService<MasterVM, Master>
{
    #region Methods

    public Task<IEnumerable<SelectListItem>> GetSelectListAsync();

    #endregion
}
