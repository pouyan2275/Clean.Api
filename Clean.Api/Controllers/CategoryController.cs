using Api.Bases.Controllers;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Dtos.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CrudController<CategoryDto, CategoryDtoSelect, Category>
    {
        public CategoryController(ICategoryRepository repository) : base(repository)
        {
        }
    }
}
