using Application.Bases.Interfaces.IServices;
using Domain.Bases.Interfaces.Entities;
using Domain.Bases.Interfaces.Repositories;
using Mapster;

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
}
