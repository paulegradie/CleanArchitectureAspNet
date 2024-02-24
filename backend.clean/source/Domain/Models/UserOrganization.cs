using Domain.Abstractions;

namespace Domain.Models;

public class UserOrganization : DomainModel
{
    public UserOrganization(User user, Organization organization)
    {
        User = user;
        Organization = organization;
    }

    public User User { get; private set; }
    public Organization Organization { get; private set; }
}