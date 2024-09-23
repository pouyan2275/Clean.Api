using Application.Bases.Dtos.Paginations;
using Application.Bases.Implements.Services;
using Application.Dtos.Categories;
using Application.IServices;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Plainquire.Page;
using System.Linq.Dynamic.Core;
namespace Application.Services;

public class CategoryService : CrudService<CategoryDto, CategoryDtoSelect, Category>, ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository repository) : base(repository)
    {
        _categoryRepository = repository;
    }
    public IEnumerable<Category> Pagination(PaginationDto paginationDto)
    {
        var table = _categoryRepository.TableNoTracking;


        paginationDto.PageNumber = paginationDto.PageNumber <= 0 ? 1 : paginationDto.PageNumber; 
        paginationDto.PageSize = paginationDto.PageSize <= 0 ? int.MaxValue : paginationDto.PageSize;

        if (paginationDto?.Filter?.Count > 0)
        {
        }

        if (paginationDto?.Sort?.Count > 0)
        {
            var sortString = "";
            paginationDto.Sort.ForEach(x => sortString += x.Key + (x.Desc ? " desc ,": " ,"));
            sortString = sortString[..(sortString.Length - 1)];
            table = table.OrderBy(sortString);
        }

        //table = table.Where(x => (x.GetType().GetProperty("CreatedOn")!.GetValue(x)) != null);
        table = table.Page(paginationDto?.PageNumber,paginationDto?.PageSize);

        return table;

    }

}
