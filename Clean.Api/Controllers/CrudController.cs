using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudController<TEntity, TId>(IRepository<TEntity, TId> repository) : CrudController<TEntity, TEntity, TEntity, TId>(repository){}
    public class CrudController<TDto,TEntity, TId>(IRepository<TEntity, TId> repository) : CrudController<TDto, TDto, TEntity, TId>(repository){}

    public class CrudController<TDto, TDtoSelect, TEntity, TId> : ControllerBase
    {
        private readonly IRepository<TEntity, TId> _repository;

        public CrudController(IRepository<TEntity, TId> repository)
        {
            _repository = repository;
        }
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> GetById(TId id, CancellationToken ct = default)
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
        public virtual async Task<ActionResult<TEntity>> Update(TId id, TEntity Tentity, CancellationToken ct = default)
        {
            var result = await _repository.UpdateAsync(id, Tentity, ct: ct);
            return Ok(result);
        }

        [HttpDelete("[action]")]
        public virtual async Task<ActionResult> Delete(TId id, CancellationToken ct = default)
        {
            await _repository.DeleteAsync(id, ct: ct);
            return Ok();
        }
    }
}
