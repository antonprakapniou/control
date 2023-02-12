namespace Control.BLL.Interfaces;

public interface IGenericService<V, T>
        where V : BaseVM
        where T : BaseModel
{
    #region Methods

    public Task<IEnumerable<V>> GetAllAsync();
    public Task<V> GetByIdAsync(Guid id);
    public Task CreateAsync(V vm);
    public Task UpdateAsync(V vm);
    public Task DeleteAsync(Guid id);

    #endregion
}