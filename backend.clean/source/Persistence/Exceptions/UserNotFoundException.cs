namespace Persistence.Exceptions;

internal class UserNotFoundException(string? message) : Exception(message)
{
}