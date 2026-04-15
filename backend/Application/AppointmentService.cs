using Application.Shared;
using CrossCutting.Interfaces;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Entities;

namespace Application;

public class AppointmentService(
    IAppointmentRepository AppointmentRepository,
    IUnitOfWorkFactory uow)
    : GenericEntityService<Appointment>(AppointmentRepository, uow), IAppointmentService;