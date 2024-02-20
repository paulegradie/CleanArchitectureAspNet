namespace Client;

public class ApiClientException : Exception
{
    public ApiClientException(string? message) : base(message)
    {
    }
}