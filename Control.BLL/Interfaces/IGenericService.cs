using Control.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.BLL.Interfaces
{
	public interface IGenericService<T> where T:class
	{
		public Task<IEnumerable<T>> GetAsync();

		public Task<T> GetByIdAsync(Guid id);

		public Task CreateAsync(T vm);

		public Task UpdateAsync(T vm);

		public Task DeleteAsync(Guid id);
	}
}
