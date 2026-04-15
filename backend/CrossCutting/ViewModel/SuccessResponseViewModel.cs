using System.Runtime.Serialization;

namespace CrossCutting.ViewModel;

public class SuccessResponseViewModel<TData>
{
    public SuccessResponseViewModel(TData data)
    {
        Data = data;
    }

    [DataMember(Name = "data")] public TData Data { get; set; }
}