namespace Api.Errors;

internal class ValidationError : ResponseError
{
    public ValidationError(IEnumerable<string> messages) : base(string.Join(MessageSeparator, messages), StatusCodes.Status400BadRequest)
    {
    }
}