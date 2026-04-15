using Api.Controllers.Shared;
using AutoMapper;
using CrossCutting.Attributes;
using Domain.Contracts.Services;
using Domain.Entities;
using Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("v1/schedule")]
[BearerAuthorize("Authenticated")]
public class ScheduleController(IScheduleService scheduleService, IMapper mapper)
    : BaseController<Schedule, ScheduleViewModel, ScheduleViewModel>(scheduleService, mapper);