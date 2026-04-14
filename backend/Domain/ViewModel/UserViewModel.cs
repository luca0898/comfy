using System.Runtime.Serialization;

namespace Comfy.Product.ViewModel
{
    public class UserViewModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "deleted")]
        public bool Deleted { get; set; }

        [DataMember(Name = "identityReference")]
        public string IdentityReference { get; set; }

        [DataMember(Name = "givenName")]
        public string GivenName { get; set; }

        [DataMember(Name = "surName")]
        public string SurName { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }
    }
}
