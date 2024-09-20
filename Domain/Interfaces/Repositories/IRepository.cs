using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity>
    {
        public IQueryable<TEntity> Table { get; }
        public IQueryable<TEntity> TableAsNoTracking { get; }
        public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);
        public Task<List<TEntity>> GetAllAsync(CancellationToken ct = default);
        public Task AddAsync(TEntity Tentity, CancellationToken ct = default);
        public void Update(TEntity Tentity);
        public Task DeleteAsync(Guid id, CancellationToken ct = default);
        public Task SaveChangesAsync(CancellationToken ct = default);
    }
}
