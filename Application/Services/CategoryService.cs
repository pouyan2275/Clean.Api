using Application.Bases;
using Application.Bases.Dtos.Paginations;
using Application.Bases.Implements.Services;
using Application.Dtos.Categories;
using Application.IServices;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Plainquire.Page;
using System.ComponentModel;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text.Json;
namespace Application.Services;

public class CategoryService : CrudService<CategoryDto, CategoryDtoSelect, Category>, ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository repository) : base(repository)
    {
        _categoryRepository = repository;
    }

    public static string GetJsonPropertyName(Enum value)
    {
        var jsonProperty = JsonSerializer.Deserialize<Enum>(value.ToString());
        return jsonProperty.ToString();
    }


}
