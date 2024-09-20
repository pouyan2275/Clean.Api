using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity>
    {
        IQueryable Table { get; }
        public IQueryable TableAsNoTracking { get; }
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<TEntity>> GetAllAsync(CancellationToken ct = default);
        Task<TEntity> AddAsync(TEntity Tentity, CancellationToken ct = default);
        Task<TEntity> UpdateAsync(Guid id, TEntity Tentity, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
