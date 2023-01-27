using Control.BLL.ViewModels;
using Control.DAL.Models;

namespace Control.BLL.Interfaces
{
	public interface IGenericService<V,T> 
		where V:BaseViewModel
		where T : BaseModel
	{
		public Task<IEnumerable<V>> GetAllAsync();
		public Task<V> GetByIdAsync(Guid id);
		public Task CreateAsync(V vm);
		public Task UpdateAsync(V vm);
		public Task DeleteAsync(Guid id);
	}
}
