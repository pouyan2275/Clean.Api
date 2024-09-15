using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity,TId>
    {
        IQueryable Table { get; }
        public IQueryable TableAsNoTracking { get; }
        Task<TEntity> GetByIdAsync(TId id, CancellationToken ct = default);
        Task<List<TEntity>> GetAllAsync(CancellationToken ct = default);
        Task<TEntity> CreateAsync(TEntity Tentity, bool saveChange = true, CancellationToken ct = default);
        Task<TEntity> UpdateAsync(TId id, TEntity Tentity, bool saveChange = true, CancellationToken ct = default);
        Task DeleteAsync(TId id, CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
