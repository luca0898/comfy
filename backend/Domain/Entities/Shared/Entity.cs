using Comfy.Product.Contracts.Shared;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Comfy.Product.Entities.Shared
{
    public class Entity : IEntity
    {
        [Key]
        public int Id { get; set; }

        [DefaultValue(false)]
        public bool Deleted { get; set; }
    }
}
