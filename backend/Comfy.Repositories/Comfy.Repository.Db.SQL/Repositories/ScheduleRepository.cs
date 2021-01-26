using Comfy.Db.SQL.Repositories.Shared;
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
    public class ScheduleRepository : GenericRelationalRepository<Schedule>, IScheduleRepository
    {

        public ScheduleRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
