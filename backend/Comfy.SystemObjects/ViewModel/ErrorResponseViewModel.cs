using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Comfy.SystemObjects.ViewModel
{
    public class ErrorResponseViewModel
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "errorCode")]
        public string ErrorCode { get; set; }

        [DataMember(Name = "errors")]
        public object Errors { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
