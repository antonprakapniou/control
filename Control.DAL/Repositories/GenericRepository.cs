namespace Control.DAL.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    #region Own fields

    //private readonly AppDbContext _db; 
    private readonly AuthDbContext _db;
    private readonly DbSet<T> _t;

    #endregion

    #region Ctor

    //public GenericRepository(AppDbContext db)
    //{
    //    _db=db;
    //    _t=_db.Set<T>();
    //}

    public GenericRepository(AuthDbContext db)
    {
        _db=db;
        _t=_db.Set<T>();
    }

    #endregion

    #region Methods

    public async Task<IEnumerable<T>> GetAllByAsync(Expression<Func<T, bool>>? expression = null)
    {
        IQueryable<T> query = _t;
        if (expression!=null) query=query.Where(expression);
        return await query.AsNoTracking().ToListAsync();
    }
    public async Task<IEnumerable<T>> GetAllByFilterAsync(params Expression<Func<T, bool>>[] expressions)
    {
        IQueryable<T> query = _t;
        var expressionsList=expressions.ToList();
        expressionsList.ForEach(_=>query=query.Where(_));
        return await query.AsNoTracking().ToListAsync();
    }
    public async Task<T> GetOneByAsync(Expression<Func<T, bool>>? expression = null)
    {
        IQueryable<T> query = _t.AsNoTracking();
        if (expression!=null) query=query.Where(expression);
        var model = await query.FirstOrDefaultAsync();
        return model!;
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

    #endregion
}