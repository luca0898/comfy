using Comfy.Product.Entities.Shared;

namespace Comfy.Product.Entities
{
    public class User : Entity
    {
        public string IdentityReference { get; private set; }
        public string GivenName { get; private set; }
        public string SurName { get; private set; }
        public string Email { get; private set; }


        public User(string identityReference, string givenName, string surName, string email)
        {
            IdentityReference = identityReference;
            GivenName = givenName;
            SurName = surName;
            Email = email;
        }
    }
}
