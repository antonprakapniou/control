namespace Control.BLL.Interfaces;

public interface IMasterService
{
    #region Methods

    public Task<IEnumerable<MasterVM>> GetAllAsync();
    public Task<MasterVM> GetByIdAsync(string id);
    public Task DeleteAsync(string id);

    #endregion
}
