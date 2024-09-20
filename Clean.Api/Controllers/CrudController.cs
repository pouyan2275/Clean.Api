using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CrudController<TEntity>(IRepository<TEntity> repository) : CrudController<TEntity, TEntity, TEntity>(repository){}
public class CrudController<TDto,TEntity>(IRepository<TEntity> repository) : CrudController<TDto, TDto, TEntity>(repository){}

public class CrudController<TDto, TDtoSelect, TEntity> : ControllerBase
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
    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TEntity?>> GetById(Guid id, CancellationToken ct = default)
    {
        var result = await _repository.GetByIdAsync(id, ct);
        return Ok(result);
    }

    /// <summary>
    /// Get All
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("[action]")]
    public virtual async Task<ActionResult<List<TEntity>>> GetAll(CancellationToken ct = default)
    {
        var result = await _repository.GetAllAsync(ct);
        return Ok(result);
    }

    /// <summary>
    /// Create New
    /// </summary>
    /// <param name="Tentity"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    public virtual async Task<ActionResult<TEntity>> Add(TEntity Tentity, CancellationToken ct = default)
    {
        Guid newId;
        bool guidUsed;
        do
        {
            newId = Guid.NewGuid();
            guidUsed =  await _repository.GetByIdAsync(newId, ct) != null;
        } while (guidUsed);

        GetType().GetProperty("CreatedOn")?.SetValue(Tentity,DateTime.UtcNow);
        GetType().GetProperty("CreatedBy")?.SetValue(Tentity,default);
        GetType().GetProperty("Id")?.SetValue(Tentity, newId);

        var result = await _repository.TableAsNoTracking.FirstAsync(x => GetType().GetProperty("Id")!.GetValue(x)!.ToString() == newId.ToString(), cancellationToken: ct);
        await _repository.AddAsync(Tentity,ct:ct);
        await _repository.SaveChangesAsync(ct);
        return Ok(result);
    }

    /// <summary>
    /// Edit a Entity
    /// </summary>
    /// <param name="id"></param>
    /// <param name="Tentity"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPut("[action]")]
    public virtual async Task<ActionResult<TEntity>> Update(Guid id, TEntity Tentity, CancellationToken ct = default)
    {
        var entity = await _repository.GetByIdAsync(id,ct);
        entity = Tentity;
        GetType().GetProperty("ModifiedOn")?.SetValue(entity, DateTime.UtcNow);
        GetType().GetProperty("ModifiedBy")?.SetValue(entity, default);
        _repository.Update(Tentity);
        await _repository.SaveChangesAsync(ct);
        var result = await _repository.TableAsNoTracking.FirstAsync(x => GetType().GetProperty("Id")!.GetValue(x)!.ToString() == id.ToString(), cancellationToken: ct);
        return Ok(result);
    }

    /// <summary>
    /// Delete a Entity
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpDelete("[action]{id}")]
    public virtual async Task<ActionResult> Delete(Guid id, CancellationToken ct = default)
    {
        await _repository.DeleteAsync(id, ct: ct);
        return Ok();
    }
}
