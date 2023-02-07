namespace Control.DAL.Interfaces;

public interface IGenericRepository<T> where T : class
{
    #region Methods

    public Task<IEnumerable<T>> GetAllByAsync(Expression<Func<T, bool>>? expression = null);
    public Task<IEnumerable<T>> GetAllByFilterAsync(params Expression<Func<T, bool>>[] expressions);
    public Task<T> GetOneByAsync(Expression<Func<T, bool>>? expression = null);
    public Task CreateAsync(T entity);
    public Task UpdateAsync(T entity);
    public Task DeleteAsync(T entity);

    #endregion
}