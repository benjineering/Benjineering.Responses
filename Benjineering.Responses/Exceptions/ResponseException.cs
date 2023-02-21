namespace Benjineering.Responses.Exceptions;

public class ResponseException : Exception
{
    public readonly IResponse Response;

    public ResponseException(IResponse response) : base(response.Message)
    {
        Response = response;
    }
}
