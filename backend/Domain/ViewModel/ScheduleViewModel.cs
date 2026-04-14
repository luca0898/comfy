using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Comfy.Product.ViewModel
{
    public class ScheduleViewModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "deleted"), DefaultValue(false)]
        public bool Deleted { get; set; }

        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "procedurePerformed"), DefaultValue(false)]
        public bool ProcedurePerformed { get; set; }
    }
}
