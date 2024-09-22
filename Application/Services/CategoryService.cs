using Application.Bases.Implements.Services;
using Application.Dtos.Categories;
using Application.IServices;
using Domain.Bases.Interfaces.Repositories;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services;

public class CategoryService : CrudService<CategoryDto, CategoryDtoSelect, Category>, ICategoryService
{
    public CategoryService(ICategoryRepository repository) : base(repository)
    {
    }
}
