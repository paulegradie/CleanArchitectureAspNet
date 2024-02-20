namespace Api.Errors;

internal class NotFoundError : ResponseError
{
    public NotFoundError(string? message) : base(message, StatusCodes.Status404NotFound)
    {
    }
}