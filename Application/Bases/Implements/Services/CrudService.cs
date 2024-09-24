using Application.Bases.Dtos.Paginations;
using Application.Bases.Interfaces.IServices;
using Domain.Bases.Interfaces.Entities;
using Domain.Bases.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using Plainquire.Page;
using System.Linq.Dynamic.Core;

namespace Application.Bases.Implements.Services;

public class CrudService<TEntity>(IRepository<TEntity> repository) : CrudService<TEntity, TEntity, TEntity>(repository)
    where TEntity : IBaseEntity{}
public class CrudService<TDto, TEntity>(IRepository<TEntity> repository) : CrudService<TDto, TDto, TEntity>(repository)
    where TEntity : IBaseEntity{}

public class CrudService<TDto, TDtoSelect, TEntity> : ICrudService<TDto, TDtoSelect, TEntity>
    where TEntity : IBaseEntity
{
    private readonly IRepository<TEntity> _repository;
    public CrudService(IRepository<TEntity> repository)
    {
        _repository = repository;
    }
    public virtual async Task<TDtoSelect> AddAsync(TDto Tentity, CancellationToken ct = default)
    {
        Guid newId;
        bool guidUsed;

        do
        {
            newId = Guid.NewGuid();
            guidUsed = await _repository.GetByIdAsync(newId, ct) != null;
        } while (guidUsed);

        var entity = Tentity.Adapt<TEntity>();

        entity!.CreatedOn = DateTime.UtcNow;
        entity!.CreatedBy = default(Guid);
        entity!.Id = newId;

        await _repository.AddAsync(entity!, ct: ct);

        var result = _repository.TableNoTracking.First(x => x.Id == newId).Adapt<TDtoSelect>();
        return result;
    }

    public virtual async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        await _repository.DeleteAsync(id, ct: ct);
    }

    public virtual async Task<List<TDtoSelect>> GetAllAsync(CancellationToken ct = default)
    {
        var result = (await _repository.GetAllAsync(ct)).Adapt<List<TDtoSelect>>();
        return result;
    }

    public virtual async Task<TDtoSelect?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var result = (await _repository.GetByIdAsync(id, ct)).Adapt<TDtoSelect>();
        return result;
    }

    public virtual async Task<TDtoSelect> UpdateAsync(Guid id, TDto Tentity, CancellationToken ct = default)
    {
        TEntity entity = await _repository.GetByIdAsync(id, ct) ?? throw new Exception("Not Found");

        entity = Tentity.Adapt(entity);

        entity!.ModifiedOn = DateTime.UtcNow;
        entity!.ModifiedBy = default(Guid);
        entity!.Id = id;

        await _repository.UpdateAsync(entity!, ct: ct);
        var result = _repository.TableNoTracking.First(x => x.Id == id).Adapt<TDtoSelect>();
        return result;
    }

    public IEnumerable<TEntity> Pagination(PaginationDto paginationDto)
    {
        var table = _repository.TableNoTracking;

        paginationDto.PageNumber = paginationDto.PageNumber <= 0 ? 1 : paginationDto.PageNumber;
        paginationDto.PageSize = paginationDto.PageSize <= 0 ? int.MaxValue : paginationDto.PageSize;

        if (paginationDto?.Filter?.Count > 0)
        {
            var sortString = "x => ";
            paginationDto.Filter.ForEach(x => {
                switch (x.Operator)
                {
                    case FilterOperator.Contains:
                        sortString += $" x.{x.Key}.ToString().Contains(\"{x.Value}\") and";
                        break;
                    case FilterOperator.IsNull:
                        sortString += $" x.{x.Key} == null and";
                        break;
                    case FilterOperator.NotNull:
                        sortString += $" x.{x.Key} != null and";
                        break;
                    default:
                        sortString += $" x.{x.Key} {x.Operator.AttributeDescription()} \"{x.Value}\" and";
                        break;
                }
            });
            sortString = sortString[..(sortString.Length - 3)];
            table = table.Where(sortString);
        }

        if (paginationDto?.Sort?.Count > 0)
        {
            var sortString = "";
            paginationDto.Sort.ForEach(x => sortString += x.Key + (x.Desc ? " desc ," : " ,"));
            sortString = sortString[..(sortString.Length - 1)];
            table = table.OrderBy(sortString);
        }

        table = table.Page(paginationDto?.PageNumber, paginationDto?.PageSize);

        return table;

    }
}
