namespace Control.BLL.Interfaces;

public interface IMasterService
{
    #region Methods

    public Task<IEnumerable<IdentityMasterVM>> GetAllAsync();
    public Task<IdentityMasterVM> GetByIdAsync(Guid id);
    public Task DeleteAsync(Guid id);

    #endregion
}
