using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Comfy.SystemObjects.ViewModel
{
    public class ErrorResponseViewModel
    {
        [DataMember(Name = "statusCode")]
        public int StatusCode { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
