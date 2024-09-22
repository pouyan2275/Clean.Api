using Application.Bases.Dtos.Paginations;
using Application.Bases.Implements.Services;
using Application.Dtos.Categories;
using Application.IServices;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Plainquire.Page;
using Plainquire.Sort;
using System.Linq.Expressions;

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
        IQueryable<Category> result;
        var sort = new EntitySort<Category>();

        paginationDto.PageNumber = paginationDto.PageNumber <= 0 ? 1 : paginationDto.PageNumber; 
        paginationDto.PageSize = paginationDto.PageSize <= 0 ? int.MaxValue : paginationDto.PageSize;

        if (paginationDto?.Filter?.Count > 0)
        {
        }
        if (paginationDto?.Sort?.Count > 0)
        {
            sort.Add(x => x.GetType().GetProperty("CreatedOn")! , paginationDto.Sort[0].Desc ? SortDirection.Descending : SortDirection.Ascending);
        }
        //sort
        //        .Add(x => x.CreatedOn, SortDirection.Descending)
        //        .Add(x => x.Title, SortDirection.Descending);

        result = _categoryRepository.TableNoTracking.OrderBy(sort).Page(paginationDto.PageNumber,paginationDto.PageSize);
        //var requestedOrders = _categoryRepository.TableNoTracking.Where(filter).OrderBy(sort).Page(page);

        return result;

    }

}
