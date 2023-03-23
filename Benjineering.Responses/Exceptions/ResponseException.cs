namespace Benjineering.Responses.Exceptions;

public class ResponseException : Exception
{
    public readonly IResponse Response;

    public ResponseException(IResponse response, string? message = null) 
        : base(message == null ? response.Message : $"{message}: {response.Message}")
    {
        Response = response;
    }
}
