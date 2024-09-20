using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity :class
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<TEntity> Entity;
    public IQueryable<TEntity> Table { get {return _dbContext.Set<TEntity>().AsTracking(); } }
    public IQueryable<TEntity> TableAsNoTracking { get { return _dbContext.Set<TEntity>().AsNoTracking(); } }

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Entity = _dbContext.Set<TEntity>();
    }

    public virtual async Task AddAsync(TEntity Tentity,CancellationToken ct = default)
    {   
        var result = await Entity.AddAsync(Tentity,ct);
    }

    public virtual async Task SaveChangesAsync( CancellationToken ct = default)
    {
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken ct = default)
    {
        var result = await Entity.ToListAsync();

        return result;
    }
    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var record = await Entity.FirstOrDefaultAsync(x => x.GetType().GetProperty("Id")!.GetValue(x)!.ToString() == id!.ToString(),ct);
        return record;
    }

    public virtual void Update(TEntity Tentity)
    {
        Entity.Update(Tentity);
    }

    public virtual async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var record = await GetByIdAsync(id);
        var result = Entity.Remove(record!);
    }
}
