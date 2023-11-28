using System.Text.Json;
using System.Text.Json.Serialization;

namespace Client;

public class ErrorResponse
{
    [JsonConstructor]
#pragma warning disable CS8618
    public ErrorResponse()
#pragma warning restore CS8618
    {
    }

    public ErrorResponse(string message)
    {
        Messages = new List<string>() { message };
    }

    public ErrorResponse(IEnumerable<string> messages)
    {
        Messages = messages;
    }

    public IEnumerable<string> Messages { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}