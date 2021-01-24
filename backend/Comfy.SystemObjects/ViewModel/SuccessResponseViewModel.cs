using System.Runtime.Serialization;

namespace Comfy.SystemObjects.ViewModel
{
    public class SuccessResponseViewModel<TData> : SuccessResponseViewModel
    {
        [DataMember(Name = "data")]
        public TData Data { get; set; }

        public SuccessResponseViewModel(TData data)
        {
            Data = data;
        }
    }

    public class SuccessResponseViewModel
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
