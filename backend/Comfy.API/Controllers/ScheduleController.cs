using AutoMapper;
using Comfy.Controllers.Shared;
using Comfy.Product.Contracts.Services;
using Comfy.Product.Entities;
using Comfy.Product.ViewModel;
using Comfy.SystemObjects.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.Controllers
{
    [Route("v1/schedule"), BearerAuthorize("Authenticated")]
    public class ScheduleController : BaseController<Schedule, ScheduleViewModel, ScheduleViewModel>
    {
        public ScheduleController(
            IScheduleService scheduleService,
            IMapper mapper)
            : base(scheduleService, mapper)
        {
        }
    }
}
