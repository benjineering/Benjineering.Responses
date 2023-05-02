using Benjineering.Responses.Errors;

namespace Benjineering.Responses.Mvc.Models;

public record ErrorResultContent
{
    public string Message { get; init; } = string.Empty;

    public Error[] Errors { get; init; } = Array.Empty<Error>();

    public ValidationError[] ValidationErrors { get; init; } = Array.Empty<ValidationError>();

    public ErrorResultContent() { }

    public ErrorResultContent(IResponse response, string? errorMessageOverride)
    {
        if (errorMessageOverride == null)
        {
            Message = response.Message;
            Errors = response.Errors;
            ValidationErrors = response.ValidationErrors;
            return;
        }

        Message = errorMessageOverride;
        ValidationErrors = response.ValidationErrors;
    }
}
