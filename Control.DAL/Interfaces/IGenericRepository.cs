using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Control.DAL.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		public Task<IEnumerable<T>> GetAllByAsync(
			Expression<Func<T, bool>>? expression = null,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        public Task<T> GetOneByAsync(
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        public void Create(T entity);
		public void Update(T entity);
		public void Delete(T entity);
		public bool IsExists(Expression<Func<T, bool>> expression);
	}
}
