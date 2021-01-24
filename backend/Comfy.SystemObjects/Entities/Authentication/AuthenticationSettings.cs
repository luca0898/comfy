namespace Comfy.SystemObjects.Entities.Authentication
{
    public class AuthenticationSettings
    {
        public string PublicKey { get; set; }
        public KeycloakSettings KeycloakSettings { get; set; }
    }
}
