using System.Runtime.Serialization;
using System.Text.Json;

namespace CrossCutting.ViewModel;

public class ErrorResponseViewModel
{
    [DataMember(Name = "message")] public required string Message { get; set; }

    [DataMember(Name = "errorCode")] public required string ErrorCode { get; set; }

    [DataMember(Name = "errors")] public required object Errors { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}