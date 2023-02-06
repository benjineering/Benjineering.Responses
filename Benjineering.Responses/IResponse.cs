using System.Text.Json.Serialization;
using Benjineering.Responses.JsonConverters;

namespace Benjineering.Responses;

[JsonConverter(typeof(IResponseJsonConverter))]
public interface IResponse
{
    bool IsOk { get; }

    ResponseType Type { get; }
    
    string Message { get; }

    IList<object> ErrorData { get; }
}

[JsonConverter(typeof(IResponseJsonConverterFactory))]
public interface IResponse<T> : IResponse
{
    T? Content { get; }
}
