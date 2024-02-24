namespace Domain.Models;

public class OrganizationUser
{
    public OrganizationUser(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; protected set; }

    public OrganizationUser UpdateUserName(string newName)
    {
        UserName = newName;
        return this;
    }
};