using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Data.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
