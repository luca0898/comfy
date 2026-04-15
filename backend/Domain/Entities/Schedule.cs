using Domain.Entities.Shared;

namespace Domain.Entities;

public class Schedule : Entity
{
    public DateTime Date { get; init; }
    public bool ProcedurePerformed { get; init; }
}