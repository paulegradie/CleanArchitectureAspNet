using Domain.Abstractions;

namespace Domain.Models;

public class Organization : DomainModel
{
    public Organization(string name, List<UserOrganization> userOrganizations)
    {
        Name = name;
        UserOrganizations = userOrganizations;
    }

    public string Name { get; private set; }

    public List<UserOrganization> UserOrganizations { get; }


    public Organization ChangeName(string newName)
    {
        Name = newName;
        return this;
    }

    public Organization AddUserOrganization(UserOrganization userOrganization)
    {
        UserOrganizations.Add(userOrganization);
        return this;
    }

    public Organization ClearUserOrganizations()
    {
        UserOrganizations.Clear();
        return this;
    }
}