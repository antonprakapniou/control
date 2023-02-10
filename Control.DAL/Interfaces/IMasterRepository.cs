namespace Control.DAL.Interfaces;

public interface IMasterRepository
{
    #region Methods

    public Task<IEnumerable<Master>> GetAllByAsync(Expression<Func<Master, bool>>? expression = null);
    public Task<Master> GetOneByAsync(Expression<Func<Master, bool>>? expression = null);
    public Task DeleteAsync(Master master);

    #endregion
}