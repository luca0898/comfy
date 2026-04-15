using Domain.Entities.Shared;

namespace Domain.Entities;

public class Appointment : Entity
{
    public DateTime Date { get; init; }
    public bool ProcedurePerformed { get; init; }
}