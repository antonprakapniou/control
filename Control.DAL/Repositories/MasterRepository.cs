namespace Control.DAL.Repositories;

public sealed class MasterRepository : IMasterRepository
{
    #region Own fields

    private readonly AuthDbContext _db;

    #endregion

    #region Ctor

    public MasterRepository(AuthDbContext db)
    {
        _db=db;
    }

    #endregion

    #region Methods

    public async Task<IEnumerable<Master>> GetAllByAsync(Expression<Func<Master, bool>>? expression = null)
    {
        IQueryable<Master> query = _db.Masters;
        if (expression!=null) query=query.Where(expression);
        return await query.AsNoTracking().ToListAsync();
    }
    public async Task<Master> GetOneByAsync(Expression<Func<Master, bool>>? expression = null)
    {
        IQueryable<Master> query = _db.Masters.AsNoTracking();
        if (expression!=null) query=query.Where(expression);
        var model = await query.FirstOrDefaultAsync();
        return model!;
    }
    public async Task DeleteAsync(Master master)
    {
        _db.Masters.Remove(master);
        await _db.SaveChangesAsync();
    }

    #endregion
}
