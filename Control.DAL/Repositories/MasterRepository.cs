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

    public async Task<IEnumerable<IdentityMaster>> GetAllByAsync(Expression<Func<IdentityMaster, bool>>? expression = null)
    {
        IQueryable<IdentityMaster> query = _db.Masters;
        if (expression!=null) query=query.Where(expression);
        return await query.AsNoTracking().ToListAsync();
    }
    public async Task<IdentityMaster> GetOneByAsync(Expression<Func<IdentityMaster, bool>>? expression = null)
    {
        IQueryable<IdentityMaster> query = _db.Masters.AsNoTracking();
        if (expression!=null) query=query.Where(expression);
        var model = await query.FirstOrDefaultAsync();
        return model!;
    }
    public async Task DeleteAsync(IdentityMaster master)
    {
        _db.Masters.Remove(master);
        await _db.SaveChangesAsync();
    }

    #endregion
}
