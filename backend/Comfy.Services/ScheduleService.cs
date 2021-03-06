﻿using Comfy.Product.Contracts.Repositories;
using Comfy.Product.Contracts.Services;
using Comfy.Product.Entities;
using Comfy.Services.Shared;
using Comfy.SystemObjects;
using Comfy.SystemObjects.Interfaces;

namespace Comfy.Service
{
    public class ScheduleService : GenericCachedEntityService<Schedule>, IScheduleService
    {
        public ScheduleService(
            ICurrentSessionUser currentSessionUser,
            ICacheProvider cacheProvider,
            IScheduleRepository scheduleRepository,
            IUnitOfWorkFactory<UnitOfWork> uow) : base(currentSessionUser, cacheProvider, scheduleRepository, uow)
        {
        }
    }
}
