using AutoMapper;
using CrossCutting.ViewModel;
using Domain.Contracts.Services.Shared;
using Domain.Contracts.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Shared;

public abstract class BaseController<TEntity, TInputViewModel, TOutputViewModel>(
    IGenericEntityService<TEntity> service,
    IMapper mapper) : Controller
    where TEntity : IEntity
{
    [HttpGet("")]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] int skip,
        [FromQuery] int take,
        CancellationToken cancellationToken)
    {
        take = take <= 0 ? 50 : take;

        var entity = await service.FindAllAsync(skip, take, cancellationToken);

        var entityView = mapper.Map<IEnumerable<TOutputViewModel>>(entity);

        return Ok(new SuccessResponseViewModel<IEnumerable<TOutputViewModel>>(entityView));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await service.GetOneAsync(id, cancellationToken);

        var entityView = mapper.Map<TOutputViewModel>(entity);

        return Ok(new SuccessResponseViewModel<TOutputViewModel>(entityView));
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateAsync([FromBody] TInputViewModel model, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<TEntity>(model);

        var newly = await service.CreateAsync(entity, cancellationToken);

        var result = mapper.Map<TOutputViewModel>(newly);

        return Ok(new SuccessResponseViewModel<TOutputViewModel>(result));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] int id,
        [FromBody] TInputViewModel model,
        CancellationToken cancellationToken)
    {
        var entity = mapper.Map<TEntity>(model);

        var updated = await service.UpdateAsync(id, entity, cancellationToken);

        var entityView = mapper.Map<TOutputViewModel>(updated);

        return Ok(new SuccessResponseViewModel<TOutputViewModel>(entityView));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        await service.DeleteAsync(id, cancellationToken);

        return Ok();
    }
}