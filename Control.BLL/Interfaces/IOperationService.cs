namespace Control.BLL.Interfaces;

public interface IOperationService : IGenericService<OperationVM, Operation>
{
    #region Methods

    public Task<IEnumerable<SelectListItem>> GetSelectListAsync();

    #endregion
}
