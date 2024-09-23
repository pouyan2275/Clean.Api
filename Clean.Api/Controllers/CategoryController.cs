using Api.Bases.Controllers;
using Application.Bases.Dtos.Paginations;
using Application.Dtos.Categories;
using Application.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CrudController<CategoryDto, CategoryDtoSelect, Category>
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) : base(categoryService)
        {
            _categoryService = categoryService;
        }
    }
}
