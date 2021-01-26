using Comfy.PRODUCT.Contracts.Repositories;
using Comfy.PRODUCT.Contracts.Services;
using Comfy.PRODUCT.Entities;
using Comfy.Services.Shared;
using Comfy.SystemObjects;
using Comfy.SystemObjects.Interfaces;

namespace Comfy.Service
{
    public class ScheduleService : GenericEntityService<Schedule>, IScheduleService
    {
        private readonly IUnitOfWorkFactory<UnitOfWork> _uow;
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleService(
            IScheduleRepository scheduleRepository,
            IUnitOfWorkFactory<UnitOfWork> uow) : base(scheduleRepository, uow)
        {
            _scheduleRepository = scheduleRepository;
            _uow = uow;
        }

    }
}
