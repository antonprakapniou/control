namespace Control.BLL.Interfaces
{
	public interface IGenericService<T> where T:class
	{
		public Task<IEnumerable<T>> GetAllAsync();
		public Task<T> GetByIdAsync(Guid id);
		public Task CreateAsync(T vm);
		public Task UpdateAsync(T vm);
		public Task DeleteAsync(Guid id);
	}
}
