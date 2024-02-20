namespace Api.Domain.Models;

[Entity]
public class UserOrganization
{
#pragma warning disable CS8618
    public UserOrganization()
#pragma warning restore CS8618
    {
    }

    public UserOrganization(ApplicationUser user, Organization organization)
    {
        ApplicationUser = user;
        Organization = organization;
    }

    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }
}