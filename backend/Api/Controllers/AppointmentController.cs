using Api.Controllers.Shared;
using AutoMapper;
using CrossCutting.Attributes;
using Domain.Contracts.Services;
using Domain.Entities;
using Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("v1/Appointment")]
[BearerAuthorize("Authenticated")]
public class AppointmentController(IAppointmentService AppointmentService, IMapper mapper)
    : BaseController<Appointment, AppointmentViewModel, AppointmentViewModel>(AppointmentService, mapper);