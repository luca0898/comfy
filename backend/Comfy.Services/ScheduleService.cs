using Comfy.PRODUCT.Contracts.Repositories;
using Comfy.PRODUCT.Contracts.Services;
using Comfy.PRODUCT.Entities;
using Comfy.SystemObjects;
using Comfy.SystemObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Comfy.Service
{
    public class ScheduleService : IScheduleService
    {
        private readonly IUnitOfWorkFactory<UnitOfWork> _uow;
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleService(IScheduleRepository scheduleRepository, IUnitOfWorkFactory<UnitOfWork> uow)
        {
            _scheduleRepository = scheduleRepository;
            _uow = uow;
        }

        public async Task<IEnumerable<Schedule>> FindAll(CancellationToken cancellationToken, int skip = 0, int take = 20)
        {
            return await _scheduleRepository.FindAll(cancellationToken, skip, take);
        }

        public async Task<Schedule> GetOne(int id)
        {
            return await _scheduleRepository.FindOne(id);
        }

        public async Task<Schedule> Create(Schedule entity)
        {
            using (var uow = _uow.Create())
            {
                Schedule result = await _scheduleRepository.Create(entity);
                await uow.CommitAsync();

                return result;
            }
        }
        public async Task<Schedule> Update(Schedule entity)
        {
            using (var uow = _uow.Create())
            {
                Schedule schedule = await _scheduleRepository.FindOne(entity.Id);

                if (schedule != null && schedule.Id > 0)
                {
                    var result = await _scheduleRepository.Update(entity);
                    await uow.CommitAsync();

                    return result;
                }
                else
                {
                    throw new ArgumentException($"Schedule {entity.Id} not found");
                }
            }
        }
        public async Task Delete(int id)
        {
            using (var uow = _uow.Create())
            {
                Schedule schedule = await _scheduleRepository.FindOne(id);

                if (schedule != null && schedule.Id > 0)
                {
                    await _scheduleRepository.SoftDelete(schedule);
                    await uow.CommitAsync();
                }
                else
                {
                    throw new ArgumentException($"Schedule {id} not found");
                }
            }
        }
    }
}
