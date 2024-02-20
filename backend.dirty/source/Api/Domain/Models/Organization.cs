namespace Api.Domain.Models;

[Entity]
public class Organization
{
#pragma warning disable CS8618
    public Organization()
#pragma warning restore CS8618
    {
        
    }
    public Organization(string name)
    {
        Name = name;
        UserOrganizations = new List<UserOrganization>();
    }

    public int OrganizationId { get; set; }
    public string Name { get; set; }
    public List<UserOrganization> UserOrganizations { get; set; }

    public void AddUserOrganization(UserOrganization userOrganization)
    {
        UserOrganizations.Add(userOrganization);
    }
}