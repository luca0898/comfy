using Comfy.Product.Contracts.Repositories;
using Comfy.Product.Contracts.Services;
using Comfy.Product.Entities;
using Comfy.Services.Shared;
using Comfy.SystemObjects;
using Comfy.SystemObjects.Interfaces;

namespace Comfy.Service
{
    public class ScheduleService : GenericEntityService<Schedule>, IScheduleService
    {
        public ScheduleService(
            IScheduleRepository scheduleRepository,
            IUnitOfWorkFactory<UnitOfWork> uow) : base(scheduleRepository, uow)
        {
        }
    }
}
