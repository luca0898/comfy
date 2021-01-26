using System;

namespace Comfy.PRODUCT.Entities
{
    public class Schedule
    {
        public int Id { get; set; }

        public bool Deleted { get; set; }

        public DateTime Date { get; set; }

        public bool ProcedurePerformed { get; set; }
    }
}
