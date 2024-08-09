using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity,TId>
    {
        Task<TEntity> GetById(TId id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Create(TEntity Tentity);
        Task<TEntity> Update(TId id, TEntity Tentity);
        Task<Guid> Delete(TId id);
    }
}
