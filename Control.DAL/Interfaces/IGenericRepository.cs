using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Control.DAL.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		public Task<IEnumerable<T>> GetAllAsync(
			Expression<Func<T, bool>>? expression = null,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
			bool isTracking = true);
		public Task<T> GetOneAsync(
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool isTracking = true);

        public void Create(T entity);
		public void Update(T entity);
		public void Delete(T entity);
	}
}
