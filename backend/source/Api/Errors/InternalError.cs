namespace Api.Errors;

internal class InternalError : ResponseError
{
    public InternalError(string? message) : base(message, StatusCodes.Status500InternalServerError)
    {
    }
}