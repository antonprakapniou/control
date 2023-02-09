namespace Control.DAL.Interfaces;

public interface IMasterRepository
{
    #region Methods

    public Task<IEnumerable<IdentityMaster>> GetAllByAsync(Expression<Func<IdentityMaster, bool>>? expression = null);
    public Task<IdentityMaster> GetOneByAsync(Expression<Func<IdentityMaster, bool>>? expression = null);
    public Task DeleteAsync(IdentityMaster master);

    #endregion
}