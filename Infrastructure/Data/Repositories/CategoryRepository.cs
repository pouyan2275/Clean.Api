using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Bases.Data;
using Infrastructure.Bases.Data.Repositories;

namespace Infrastructure.Data.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
