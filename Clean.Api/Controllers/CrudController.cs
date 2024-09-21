using Domain.Interfaces.Repositories;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
/// <summary>
/// Restfull api
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <param name="repository"></param>
[Route("api/[controller]")]
[ApiController]
public class CrudController<TEntity>(IRepository<TEntity> repository) : CrudController<TEntity, TEntity, TEntity>(repository) where TEntity : new() { }
public class CrudController<TDto,TEntity>(IRepository<TEntity> repository)  : CrudController<TDto, TDto, TEntity>  (repository) where TEntity : new() { }

public class CrudController<TDto, TDtoSelect, TEntity> : ControllerBase
    where TEntity : new()
{
    private readonly IRepository<TEntity> _repository;

    public CrudController(IRepository<TEntity> repository)
    {
        _repository = repository;
    }
    /// <summary>
    /// Get By Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("[action]/{id}")]
    public virtual async Task<ActionResult<TDtoSelect?>> GetById(Guid id, CancellationToken ct = default)
    {
        var result = (await _repository.GetByIdAsync(id, ct)).Adapt<TDtoSelect>();
        return Ok(result);
    }

    /// <summary>
    /// Get All
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("[action]")]
    public virtual async Task<ActionResult<List<TDtoSelect>>> GetAll(CancellationToken ct = default)
    {
        var result = (await _repository.GetAllAsync(ct)).Adapt<List<TDtoSelect>>();
        return Ok(result);
    }

    /// <summary>
    /// Create New
    /// </summary>
    /// <param name="Tentity"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    public virtual async Task<ActionResult<TDtoSelect>> Add(TDto Tentity, CancellationToken ct = default)
    {
        Guid newId;
        bool guidUsed;

        do
        {
            newId = Guid.NewGuid();
            guidUsed =  await _repository.GetByIdAsync(newId, ct) != null;
        } while (guidUsed);

        var entity = Tentity.Adapt<TEntity>();

        entity?.GetType().GetProperty("CreatedOn")?.SetValue(entity, DateTime.UtcNow);
        entity?.GetType().GetProperty("CreatedBy")?.SetValue(entity, default(Guid));
        entity?.GetType().GetProperty("Id")?.SetValue(entity, newId);
 
        await _repository.AddAsync(entity, ct:ct);

        var result = await _repository.GetByIdAsync(newId,ct);

        return Ok(result);
    }

    /// <summary>
    /// Edit a Entity
    /// </summary>
    /// <param name="id"></param>
    /// <param name="Tentity"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPut("[action]/{id}")]
    public virtual async Task<ActionResult<TDtoSelect>> Update(Guid id, TDto Tentity, CancellationToken ct = default)
    {
        TEntity entity = await _repository.GetByIdAsync(id, ct) ?? throw new Exception("Not Found");        

        entity = Tentity.Adapt(entity);

        entity?.GetType().GetProperty("ModifiedOn")?.SetValue(entity, DateTime.UtcNow);
        entity?.GetType().GetProperty("ModifiedBy")?.SetValue(entity, default(Guid));
        entity?.GetType().GetProperty("Id")?.SetValue(entity, id);

        await _repository.UpdateAsync(entity,ct:ct);
        var result = await _repository.GetByIdAsync(id, ct);
        return Ok(result);
    }

    /// <summary>
    /// Delete a Entity
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpDelete("[action]/{id}")]
    public virtual async Task<ActionResult> Delete(Guid id, CancellationToken ct = default)
    {
        await _repository.DeleteAsync(id, ct: ct);
        return Ok();
    }
}
