using Comfy.PRODUCT.Contracts.Repositories;
using Comfy.PRODUCT.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Comfy.REPOSITORIES
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<Schedule> _schedules;

        public ScheduleRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _schedules = _dbContext.Set<Schedule>();
        }

        public async Task<IEnumerable<Schedule>> FindAll(CancellationToken cancellationToken, int skip = 0, int take = 20)
        {
            return await Task.Run(() =>
            {
                return _schedules
                    .AsQueryable()
                    .Where(schedule => schedule.Deleted == false)
                    .OrderBy(schedule => schedule.Date)
                    .Skip(skip)
                    .Take(take);

            }, cancellationToken);
        }

        public async Task<Schedule> FindOne(int id, CancellationToken cancellationToken)
        {
            return await _schedules.FindAsync(id, cancellationToken);
        }

        public async Task<Schedule> Create(Schedule entity, CancellationToken cancellationToken)
        {
            EntityEntry<Schedule> newly = await _schedules.AddAsync(entity);

            await SaveChangesAsync(cancellationToken);

            return newly.Entity;
        }
        public async Task<Schedule> Update(Schedule entity, CancellationToken cancellationToken)
        {
            EntityEntry<Schedule> updatedEntity = _schedules.Update(entity);

            await SaveChangesAsync(cancellationToken);

            return updatedEntity.Entity;
        }
        public async Task SoftDelete(Schedule entity, CancellationToken cancellationToken)
        {
            entity.Deleted = true;

            _schedules.Update(entity);

            await SaveChangesAsync(cancellationToken);
        }
        public async Task HardDelete(Schedule entity, CancellationToken cancellationToken)
        {
            _schedules.Remove(entity);

            await SaveChangesAsync(cancellationToken);
        }

        private async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            int result = await _dbContext.SaveChangesAsync(cancellationToken);

            if (result > 0)
                throw new Exception("Cannot save changes");
        }
    }
}
