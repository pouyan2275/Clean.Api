using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

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
    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TEntity>> GetById(Guid id, CancellationToken ct = default)
    {
        var result = await _repository.GetByIdAsync(id, ct);
        return Ok(result);
    }

    [HttpGet("[action]")]
    public virtual async Task<ActionResult<List<TEntity>>> GetAll(CancellationToken ct = default)
    {
        var result = await _repository.GetAllAsync(ct);
        return Ok(result);
    }

    [HttpPost("[action]")]
    public virtual async Task<ActionResult<TEntity>> Add(TEntity Tentity, CancellationToken ct = default)
    {
        var result = await _repository.AddAsync(Tentity,ct:ct);
        return Ok(result);
    }

    [HttpPut("[action]")]
    public virtual async Task<ActionResult<TEntity>> Update(Guid id, TEntity Tentity, CancellationToken ct = default)
    {
        var result = await _repository.UpdateAsync(id, Tentity, ct: ct);
        return Ok(result);
    }

    [HttpDelete("[action]")]
    public virtual async Task<ActionResult> Delete(Guid id, CancellationToken ct = default)
    {
        await _repository.DeleteAsync(id, ct: ct);
        return Ok();
    }
}
