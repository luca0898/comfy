using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Comfy.PRODUCT.Contracts.Services;
using Comfy.PRODUCT.Entities;
using Comfy.PRODUCT.ViewModel;
using Comfy.SystemObjects.ViewModel;
using System.Threading;

namespace Comfy.Controllers
{
    [Route("v1/schedules")]
    public class ScheduleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService, IMapper mapper)
        {
            _scheduleService = scheduleService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All schedules
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        [SwaggerOperation(OperationId = "{entity}GetAll")]
        public async Task<IActionResult> Get([FromQuery] int skip, [FromQuery] int take, CancellationToken cancellationToken)
        {
            take = (take <= 0) ? 50 : take;

            IEnumerable<Schedule> schedule = await _scheduleService.FindAll(cancellationToken, skip, take);

            IEnumerable<ScheduleViewModel> viewSchedules = _mapper.Map<IEnumerable<ScheduleViewModel>>(schedule);

            return Ok(new SuccessResponseViewModel<IEnumerable<ScheduleViewModel>>(viewSchedules));
        }

        /// <summary>
        /// Get one schedule
        /// </summary>
        /// <param name="id">schedule identifier</param>
        /// <returns>The schedule record</returns>
        [HttpGet, Route("{id}")]
        [SwaggerOperation(OperationId = "{entity}GetById")]
        public async Task<IActionResult> Get(int id)
        {
            Schedule schedule = await _scheduleService.GetOne(id);

            ScheduleViewModel scheduleView = _mapper.Map<ScheduleViewModel>(schedule);

            return Ok(new SuccessResponseViewModel<ScheduleViewModel>(scheduleView));
        }

        /// <summary>
        /// Create a new schedule
        /// </summary>
        /// <param name="model">Schedule item</param>
        /// <returns></returns>
        [HttpPost, Route("")]
        public async Task<IActionResult> Create([FromBody] ScheduleViewModel model)
        {
            Schedule entity = _mapper.Map<Schedule>(model);

            Schedule newly = await _scheduleService.Create(entity);

            ScheduleViewModel result = _mapper.Map<ScheduleViewModel>(newly);

            return Ok(new SuccessResponseViewModel<ScheduleViewModel>(result));
        }

        /// <summary>
        /// Update a schedule
        /// </summary>
        /// <param name="model">Schedule item</param>
        /// <returns></returns>
        [HttpPut, Route("")]
        public async Task<IActionResult> Update([FromBody] ScheduleViewModel model)
        {
            Schedule entity = _mapper.Map<Schedule>(model);

            Schedule updated = await _scheduleService.Update(entity);

            ScheduleViewModel result = _mapper.Map<ScheduleViewModel>(updated);

            return Ok(new SuccessResponseViewModel<ScheduleViewModel>(result));
        }

        /// <summary>
        /// Delete a schedule
        /// </summary>
        /// <param name="id">schedule key</param>
        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _scheduleService.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponseViewModel() { Message = ex.Message, StatusCode = 404 });
            }
        }
    }
}
