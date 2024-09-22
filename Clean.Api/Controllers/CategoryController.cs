using Api.Bases.Controllers;
using Application.Bases.Interfaces.IServices;
using Application.Dtos.Categories;
using Application.IServices;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CrudController<CategoryDto, CategoryDtoSelect, Category>
    {
        public CategoryController(ICategoryService crudService) : base(crudService)
        {
        }
    }
}
