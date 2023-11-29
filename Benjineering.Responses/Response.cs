using Benjineering.Responses.Errors;
using Benjineering.Responses.Exceptions;

namespace Benjineering.Responses;

public record Response : IResponse
{
    public const string DefaultBadRequestMessage = "Invalid";

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

    public static Response BadRequest(string propertyName, string errorMessage)
    {
        var validationErrors = new[]
        {
            ValidationError.Create(propertyName, errorMessage),
        };

        return new Response(ResponseType.BadRequest, DefaultBadRequestMessage, errors: null, validationErrors);
    }

    public static Response<T> BadRequest<T>(string propertyName, string errorMessage)
    {
        var validationErrors = new[]
        {
            ValidationError.Create(propertyName, errorMessage),
        };

        return new Response<T>(ResponseType.BadRequest, DefaultBadRequestMessage, errors: null, validationErrors);
    }

    public static Response NotSignedIn(string? message = null)
    {
        return new Response(ResponseType.NotSignedIn, message ?? "Unauthorised");
    }

    public static Response<T> NotSignedIn<T>(string? message = null)
    {
        return new Response<T>(ResponseType.NotSignedIn, message ?? "Unauthorised");
    }

    public static Response NotPermitted(string? message = null)
    {
        return new Response(ResponseType.NotPermitted, message ?? "Not allowed");
    }

    public static Response<T> NotPermitted<T>(string? message = null)
    {
        return new Response<T>(ResponseType.NotPermitted, message ?? "Not allowed");
    }

    public static Response<T> FromResponse<T>(IResponse response)
    {
        return new Response<T>(response.Type, response.Message, response.Errors, response.ValidationErrors);
    }

    public void EnsureSuccess(string? message)
    {
        if (!IsOk)
            throw new ResponseException(this, message);
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

	public new T EnsureSuccess(string? message)
	{
		if (!IsOk || Content is null)
			throw new ResponseException(this, message);

        return Content;
	}
}
