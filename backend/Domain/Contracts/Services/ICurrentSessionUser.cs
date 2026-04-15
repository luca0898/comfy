namespace Domain.Contracts.Services;

public interface ICurrentSessionUser
{
    public string? Id { get; init; }
    public string? GivenName { get; init; }
    public string? SurName { get; init; }
    public string? EmailAddress { get; init; }
}