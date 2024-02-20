namespace Api.Errors;

public abstract class ResponseError : Exception
{
    public const string MessageSeparator = "\n";

    protected ResponseError(string? message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    protected ResponseError(IEnumerable<string> messages, int statusCode) : base(string.Join(MessageSeparator, messages))
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; }
}