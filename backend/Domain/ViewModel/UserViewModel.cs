using System.Runtime.Serialization;

namespace Domain.ViewModel;

public class UserViewModel
{
    [DataMember(Name = "id")] public int Id { get; set; }
    [DataMember(Name = "deleted")] public bool Deleted { get; set; }

    [DataMember(Name = "identityReference")]
    public required string IdentityReference { get; set; }

    [DataMember(Name = "givenName")] public required string GivenName { get; init; }
    [DataMember(Name = "surName")] public required string SurName { get; init; }
    [DataMember(Name = "email")] public required string Email { get; init; }
}