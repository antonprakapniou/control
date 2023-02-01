using Control.DAL.EF;
using Control.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Control.DAL.Repositories
{
	public class GenericRepository<T>:IGenericRepository<T> where T : class
	{
		private readonly AppDbContext _db;
		private readonly DbSet<T> _t;

		public GenericRepository(AppDbContext db)
		{
			_db=db;
			_t=_db.Set<T>();
		}

		public async Task<IEnumerable<T>> GetAllByAsync(
			Expression<Func<T, bool>>? expression = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
		{
			IQueryable<T> query = _t;
			if (expression!=null) query=query.Where(expression);
			if (include!=null) query=include(query);
			return await query.AsNoTracking().ToListAsync();
		}
		
		public async Task<T> GetOneByAsync(
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = _t;
            if (expression!=null) query=query.Where(expression);
            if (include!=null) query=include(query);
			var models=await query.AsNoTracking().FirstOrDefaultAsync();
			return models!;
        }

        public async Task CreateAsync(T entity)
		{
			_t.Add(entity);
			await _db.SaveChangesAsync();
		}
		public async Task UpdateAsync(T entity)
		{
			_t.Update(entity);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity)
		{
			_t.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
