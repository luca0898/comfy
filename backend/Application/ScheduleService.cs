using Application.Shared;
using CrossCutting.Interfaces;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Entities;

namespace Application;

public class ScheduleService(
    IScheduleRepository scheduleRepository,
    IUnitOfWorkFactory uow)
    : GenericEntityService<Schedule>(scheduleRepository, uow), IScheduleService;