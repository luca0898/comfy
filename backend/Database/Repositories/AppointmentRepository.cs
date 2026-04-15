using Database.Repositories.Shared;
using Domain.Contracts.Repositories;
using Domain.Entities;

namespace Database.Repositories;

public class AppointmentRepository : GenericRelationalRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(DbContext dbContext) : base(dbContext)
    {
    }
}