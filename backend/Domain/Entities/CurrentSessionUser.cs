using Domain.Contracts.Services;

namespace Domain.Entities;

public class CurrentSessionUser : ICurrentSessionUser
{
    public string? Id { get; init; }
    public string? GivenName { get; init; }
    public string? SurName { get; init; }
    public string? EmailAddress { get; init; }
}