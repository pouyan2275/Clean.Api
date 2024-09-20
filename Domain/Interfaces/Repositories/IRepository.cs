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
        public IQueryable<TEntity> TableNoTracking { get; }
        public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);
        public Task<List<TEntity>> GetAllAsync(CancellationToken ct = default);
        public Task AddAsync(TEntity Tentity, bool save = true, CancellationToken ct = default);
        public Task UpdateAsync(TEntity Tentity, bool save = true, CancellationToken ct = default);
        public Task DeleteAsync(Guid id, CancellationToken ct = default);
        public Task SaveChangesAsync(CancellationToken ct = default);
    }
}
