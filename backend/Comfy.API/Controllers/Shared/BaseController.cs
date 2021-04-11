using AutoMapper;
using Comfy.Product.Contracts.Services.Shared;
using Comfy.Product.Contracts.Shared;
using Comfy.SystemObjects.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Comfy.Controllers.Shared
{
    public abstract class BaseController<TEntity, TInputViewModel, TOutputViewModel> : Controller where TEntity : IEntity
    {
        private readonly IMapper _mapper;
        private readonly IGenericEntityService<TEntity> _service;

        public BaseController(IGenericEntityService<TEntity> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All entities
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        [SwaggerOperation(OperationId = "{entity}GetAll")]
        public async Task<IActionResult> GetAllAsync([FromQuery] int skip, [FromQuery] int take, CancellationToken cancellationToken)
        {
            take = (take <= 0) ? 50 : take;

            IEnumerable<TEntity> entity = await _service.FindAllAsync(cancellationToken, skip, take);

            IEnumerable<TOutputViewModel> entityView = _mapper.Map<IEnumerable<TOutputViewModel>>(entity);

            return Ok(new SuccessResponseViewModel<IEnumerable<TOutputViewModel>>(entityView));
        }

        /// <summary>
        /// Get one entity
        /// </summary>
        /// <param name="id">entity identifier</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The entity record</returns>
        [HttpGet, Route("{id}")]
        [SwaggerOperation(OperationId = "{entity}GetById")]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
        {
            TEntity entity = await _service.GetOneAsync(id, cancellationToken);

            TOutputViewModel entityView = _mapper.Map<TOutputViewModel>(entity);

            return Ok(new SuccessResponseViewModel<TOutputViewModel>(entityView));
        }

        /// <summary>
        /// Create a new entity
        /// </summary>
        /// <param name="model">TEntity item</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, Route("")]
        public async Task<IActionResult> CreateAsync([FromBody] TInputViewModel model, CancellationToken cancellationToken)
        {
            TEntity entity = _mapper.Map<TEntity>(model);

            TEntity newly = await _service.CreateAsync(entity, cancellationToken);

            TOutputViewModel result = _mapper.Map<TOutputViewModel>(newly);

            return Ok(new SuccessResponseViewModel<TOutputViewModel>(result));
        }

        /// <summary>
        /// Update a entity
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <param name="model">New data</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut, Route("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] TInputViewModel model, CancellationToken cancellationToken)
        {
            TEntity entity = _mapper.Map<TEntity>(model);

            TEntity updated = await _service.UpdateAsync(id, entity, cancellationToken);

            TOutputViewModel entityView = _mapper.Map<TOutputViewModel>(updated);

            return Ok(new SuccessResponseViewModel<TOutputViewModel>(entityView));
        }

        /// <summary>
        /// Delete a entity
        /// </summary>
        /// <param name="id">entity key</param>
        /// <param name="cancellationToken"></param>
        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(id, cancellationToken);

            return Ok();
        }
    }
}
