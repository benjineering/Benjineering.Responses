namespace Benjineering.Responses;

public record Response : IResponse
{
    public ResponseType Type { get; init; }

    public bool IsOk => Type == ResponseType.Success;

    public string Message { get; init; }

    public IList<object> ErrorData { get; init; }

    public Response(ResponseType type, string message, IList<object>? errorData = null)
    {
        Type = type;
        Message = message;
        ErrorData = errorData ?? Array.Empty<object>();
    }

    public static Response Success()
    {
        return new Response(ResponseType.Success, "Ok");
    }

    public static Response<T> Success<T>(T content)
    {
        return new Response<T>(ResponseType.Success, "Ok", content);
    }

    public static Response Error(string message, IList<object>? errorData = null)
    {
        return new Response(ResponseType.Error, message, errorData);
    }

    public static Response<T> Error<T>(string message, IList<object>? errorData = null)
    {
        return new Response<T>(ResponseType.Error, message, errorData);
    }

    public static Response NotFound(string message, IList<object>? errorData = null)
    {
        return new Response(ResponseType.NotFound, message, errorData);
    }

    public static Response<T> NotFound<T>(string message, IList<object>? errorData = null)
    {
        return new Response<T>(ResponseType.NotFound, message, errorData);
    }

    public static Response BadRequest(string message, IList<object>? errorData = null)
    {
        return new Response(ResponseType.BadRequest, message, errorData);
    }

    public static Response<T> BadRequest<T>(string message, IList<object>? errorData = null)
    {
        return new Response<T>(ResponseType.BadRequest, message, errorData);
    }

    public static Response<T> FromResponse<T>(IResponse response)
    {
        return new Response<T>(response.Type, response.Message, response.ErrorData);
    }
}

public record Response<T> : Response, IResponse<T>
{
    public T? Content { get; init; }

    public Response(ResponseType type, string message, IList<object>? errorData = null)
        : base(type, message, errorData) { }

    public Response(ResponseType type, string message, T content, IList<object>? errorData = null) 
        : base(type, message, errorData)
    {
        Content = content;
    }
}
