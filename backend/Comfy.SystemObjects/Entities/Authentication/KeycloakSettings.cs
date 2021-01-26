namespace Comfy.SystemObjects.Entities
{
    public class KeycloakSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Authority { get; set; }
        public string BaseUrl { get; set; }
        public string Realm { get; set; }
    }
}