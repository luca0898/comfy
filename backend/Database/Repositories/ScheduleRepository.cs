using Database.Repositories.Shared;
using Domain.Contracts.Repositories;
using Domain.Entities;

namespace Database.Repositories;

public class ScheduleRepository : GenericRelationalRepository<Schedule>, IScheduleRepository
{
    public ScheduleRepository(DbContext dbContext) : base(dbContext)
    {
    }
}