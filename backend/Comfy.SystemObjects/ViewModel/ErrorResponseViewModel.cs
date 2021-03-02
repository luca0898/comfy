using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Comfy.SystemObjects.ViewModel
{
    public class ErrorResponseViewModel
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "errorCode")]
        public string ErrorCode { get; set; }

        [DataMember(Name = "statusCode")]
        public int StatusCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
