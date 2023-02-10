namespace Control.BLL.Interfaces;

public interface IMasterService
{
    #region Methods

    public Task<IEnumerable<MasterVM>> GetAllAsync();
    public Task<MasterVM> GetByIdAsync(Guid id);
    public Task DeleteAsync(Guid id);

    #endregion
}
