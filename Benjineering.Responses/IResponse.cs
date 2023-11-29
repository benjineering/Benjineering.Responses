using System.Text.Json.Serialization;
using Benjineering.Responses.Errors;
using Benjineering.Responses.JsonConverters;

namespace Benjineering.Responses;

[JsonConverter(typeof(IResponseJsonConverter))]
public interface IResponse
{
    bool IsOk { get; }

    ResponseType Type { get; }
    
    string Message { get; }

    Error[] Errors { get; }

    ValidationError[] ValidationErrors { get; }

    /// <exception cref="ResponseException"></exception>
    void EnsureSuccess(string? message = null);
}

[JsonConverter(typeof(IResponseJsonConverterFactory))]
public interface IResponse<T> : IResponse
{
    T? Content { get; }

	new T EnsureSuccess(string? message = null);
}
