using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class Repository<Tentity, Tid> : IRepository<Tentity, Tid>
    {
        public Task<Tentity> Create(Tentity Tentity)
        {
            throw new NotImplementedException();
        }

        public Task<Tid> Delete(Tid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tentity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Tentity> GetById(Tid id)
        {
            throw new NotImplementedException();
        }

        public Task<Tentity> Update(Tid id, Tentity Tentity)
        {
            throw new NotImplementedException();
        }
    }
}
