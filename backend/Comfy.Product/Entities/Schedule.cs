using Comfy.Product.Contracts.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Comfy.PRODUCT.Entities
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
