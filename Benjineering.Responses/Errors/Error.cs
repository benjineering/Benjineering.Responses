namespace Benjineering.Responses.Errors;

public record Error
{
    public required string Message { get; init; }

    public string? Code { get; init; }

    public Error() { }

    public static Error Create(string message, string? code = null)
    {
        return new Error
        { 
            Message = message, 
            Code = code 
        };
    }
}
