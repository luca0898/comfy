using System;
using System.Runtime.Serialization;

namespace Comfy.Product.ViewModel
{
    public class ScheduleViewModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "deleted")]
        public bool Deleted { get; set; }

        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "procedurePerformed")]
        public bool ProcedurePerformed { get; set; }
    }
}
