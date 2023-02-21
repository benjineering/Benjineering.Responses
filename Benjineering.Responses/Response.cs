using Benjineering.Responses.Errors;

namespace Benjineering.Responses;

public record Response : IResponse
{
    public ResponseType Type { get; init; }

    public bool IsOk => Type == ResponseType.Success;

    public string Message { get; init; }

    public Error[] Errors { get; init; }

    public ValidationError[] ValidationErrors { get; init; }

    public Response(ResponseType type, string message, Error[]? errors = null, ValidationError[]? validationErrors = null)
    {
        Type = type;
        Message = message;
        Errors = errors ?? Array.Empty<Error>();
        ValidationErrors = validationErrors ?? Array.Empty<ValidationError>();
    }

    public static Response Success()
    {
        return new Response(ResponseType.Success, "Ok");
    }

    public static Response<T> Success<T>(T content)
    {
        return new Response<T>(ResponseType.Success, "Ok", content);
    }

    public static Response Error(string message, Error[]? errors = null)
    {
        return new Response(ResponseType.Error, message, errors);
    }

    public static Response<T> Error<T>(string message, Error[]? errors = null)
    {
        return new Response<T>(ResponseType.Error, message, errors);
    }

    public static Response NotFound(string message, Error[]? errors = null)
    {
        return new Response(ResponseType.NotFound, message, errors);
    }

    public static Response<T> NotFound<T>(string message, Error[]? errors = null)
    {
        return new Response<T>(ResponseType.NotFound, message, errors);
    }

    public static Response BadRequest(string message, ValidationError[]? validationErrors = null)
    {
        return new Response(ResponseType.BadRequest, message, errors: null, validationErrors);
    }

    public static Response<T> BadRequest<T>(string message, ValidationError[]? validationErrors = null)
    {
        return new Response<T>(ResponseType.BadRequest, message, errors: null, validationErrors);
    }

    public static Response<T> FromResponse<T>(IResponse response)
    {
        return new Response<T>(response.Type, response.Message, response.Errors, response.ValidationErrors);
    }
}

public record Response<T> : Response, IResponse<T>
{
    public T? Content { get; init; }

    public Response(ResponseType type, string message, Error[]? errors = null, ValidationError[]? validationErrors = null)
        : base(type, message, errors, validationErrors) { }

    public Response(ResponseType type, string message, T content, Error[]? errors = null, ValidationError[]? validationErrors = null) 
        : base(type, message, errors, validationErrors)
    {
        Content = content;
    }
}
