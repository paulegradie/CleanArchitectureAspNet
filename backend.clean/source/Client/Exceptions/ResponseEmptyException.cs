namespace Client.Exceptions;

public class ResponseEmptyException : Exception
{
    public ResponseEmptyException(string? message) : base(message)
    {
    }
}