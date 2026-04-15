using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Contracts.Shared;

namespace Domain.Entities.Shared;

public class Entity : IEntity
{
    [Key] public int Id { get; set; }

    [DefaultValue(false)] public bool Deleted { get; set; }
}