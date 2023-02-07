namespace Control.BLL.Interfaces;

public interface ICategoryService : IGenericService<CategoryVM, Category>
{
    #region Methods

    public Task<IEnumerable<SelectListItem>> GetSelectListAsync();

    #endregion
}