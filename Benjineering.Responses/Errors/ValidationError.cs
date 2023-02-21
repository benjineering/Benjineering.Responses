namespace Benjineering.Responses.Errors;

public record ValidationError
{
    public required string PropertyName { get; init; }

    public required Error[] Errors { get; init; }

    public ValidationError() { }

    public static ValidationError Create(string propertyName, string errorMessage)
    {
        return new ValidationError
        {
            PropertyName = propertyName,
            Errors = new[] { Error.Create(errorMessage) },
        };
    }
}
