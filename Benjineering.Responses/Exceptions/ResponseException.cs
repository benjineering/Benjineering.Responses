namespace Benjineering.Responses.Exceptions;

public class ResponseException : Exception
{
    public readonly IResponse Response;

    public ResponseException(IResponse response, string? message = null) 
        : base(BuildMessage(response, message))
    {
        Response = response;
    }

    private static string BuildMessage(IResponse response, string? message = null)
    {
        var str = message == null ? response.Message : $"{message}: {response.Message}";

        if (response.ValidationErrors.Any())
        {
            var validationErrors = response.ValidationErrors
                .Select(x => $"\n\t\t{x.PropertyName}: {string.Join(string.Empty, x.Errors.Select(x => $"\n\t\t\t{x.Message}"))}");

            str += "\n\tValidationErrors:" + validationErrors;
        }

        if (response.Errors.Any())
        {
            var errors = response.Errors
                .Select(x => $"\n\t\t{x.Message}");

            str += "\n\tErrors:" + errors;
        }

        return str;
    }
}
