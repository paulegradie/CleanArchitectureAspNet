namespace Authentication.Abstractions.AccessPolicies;

public interface IJwt
{
    string GenerateJwtToken(bool isAdmin, string userName);
}