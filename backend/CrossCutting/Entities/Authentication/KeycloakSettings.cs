namespace CrossCutting.Entities.Authentication;

public sealed class KeycloakSettings
{
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required string Authority { get; set; }
    public required string BaseUrl { get; set; }
    public required string Realm { get; set; }
}