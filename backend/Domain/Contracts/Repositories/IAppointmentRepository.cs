using Domain.Contracts.Repositories.Shared;
using Domain.Entities;

namespace Domain.Contracts.Repositories;

public interface IAppointmentRepository : IGenericRepository<Appointment>
{
}