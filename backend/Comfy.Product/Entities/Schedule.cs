using Comfy.Product.Contracts.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace Comfy.Product.Entities
{
    public class Schedule : IEntity
    {
        [Key]
        public int Id { get; set; }

        public bool Deleted { get; set; }

        public DateTime Date { get; set; }

        public bool ProcedurePerformed { get; set; }
    }
}
