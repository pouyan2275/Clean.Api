using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CrudController<Category, Category, Category, Guid>
    {
        public CategoryController(ICategoryRepository repository) : base(repository)
        {
        }
    }
}
