namespace Control.BLL.Interfaces;

public interface IOwnerService : IGenericService<OwnerVM, Owner>
{
    #region Methods

    public Task<IEnumerable<SelectListItem>> GetSelectListAsync();

    #endregion
}
