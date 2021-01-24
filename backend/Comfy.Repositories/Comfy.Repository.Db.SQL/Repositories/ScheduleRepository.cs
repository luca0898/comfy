using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comfy.Db.SQL;
using Comfy.PRODUCT.Contracts.Repositories;
using Comfy.PRODUCT.Entities;
using System.Threading;

namespace Comfy.REPOSITORIES
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly DbContext _dbContext;
        public ScheduleRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Schedule>> FindAll(CancellationToken cancellationToken, int skip = 0, int take = 20)
        {
            return await Task.Run(() =>
            {
                return _dbContext.Set<Schedule>()
                    .AsQueryable()
                    .Where(schedule => schedule.Deleted == false)
                    .OrderBy(o => o.Date)
                    .Skip(skip)
                    .Take(take);

            }, cancellationToken);
        }

        public async Task<Schedule> FindOne(int id)
        {
            return await _dbContext.Set<Schedule>().FindAsync(id);
        }

        public async Task<Schedule> Create(Schedule entity)
        {
            var newly = await _dbContext.AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(newly.Entity);
        }
        public async Task<Schedule> Update(Schedule entity)
        {
            var updatedEntity = _dbContext.Set<Schedule>().Update(entity);

            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(updatedEntity.Entity);
        }
        public async Task SoftDelete(Schedule entity)
        {
            entity.Deleted = true;

            _dbContext.Set<Schedule>().Update(entity);

            await _dbContext.SaveChangesAsync();
        }
        public async Task HardDelete(Schedule entity)
        {
            _dbContext.Set<Schedule>().Remove(entity);

            await _dbContext.SaveChangesAsync();
        }
    }
}
