using Comfy.Product.Entities.Shared;
using System;

namespace Comfy.Product.Entities
{
    public class Schedule : Entity
    {
        public DateTime Date { get; set; }

        public bool ProcedurePerformed { get; set; }
    }
}
