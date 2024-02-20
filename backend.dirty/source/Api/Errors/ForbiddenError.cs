namespace Api.Errors;

internal class ForbiddenError : ResponseError
{
    public ForbiddenError(string? message) : base(message, StatusCodes.Status403Forbidden)
    {
    }
}