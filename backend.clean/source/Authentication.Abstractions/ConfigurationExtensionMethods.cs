using Microsoft.Extensions.Configuration;

namespace Authentication.Abstractions;

public static class ConfigurationExtensionMethods
{
    public static string Environment(this IConfiguration configuration)
    {
        return configuration.GetValue<string>("Environment") ?? throw new Exception("Could not find Environment setting");
    }

    public static string Jwt(this IConfiguration configuration)
    {
        return configuration.GetValue<string>("Jwt:Key") ?? throw new Exception("Failed to find Jwt key");
    }
}