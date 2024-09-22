using Api.Bases.Controllers;
using Application.Dtos.Categories;
using Domain.Entities;
using Domain.Interfaces.Repositories;
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
