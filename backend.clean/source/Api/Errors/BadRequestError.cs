namespace Api.Errors;

internal class BadRequestError : ResponseError
{
    public BadRequestError(string? message) : base(message, StatusCodes.Status400BadRequest)
    {
    }
}