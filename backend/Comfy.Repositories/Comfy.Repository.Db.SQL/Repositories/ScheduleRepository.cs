using Comfy.Db.SQL.Repositories.Shared;
using Comfy.Product.Contracts.Repositories;
using Comfy.Product.Entities;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Db.SQL.Repositories
{
    public class ScheduleRepository : GenericRelationalRepository<Schedule>, IScheduleRepository
    {

        public ScheduleRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
