namespace Comfy.Product.Contracts.Services
{
    public interface ICurrentSessionUser
    {
        public string Id { get; set; }
        public string GivenName { get; set; }
        public string SurName { get; set; }
        public string EmailAddress { get; set; }
    }
}
