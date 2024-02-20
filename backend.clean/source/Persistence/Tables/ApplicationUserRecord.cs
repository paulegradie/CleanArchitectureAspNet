using Microsoft.AspNetCore.Identity;

namespace Persistence.Tables;

// [DatabaseModel]
public class ApplicationUserRecord : IdentityUser
{
    public ApplicationUserRecord()
    {
    }

    public ApplicationUserRecord(bool isAdmin, string userName)
    {
        IsAdmin = isAdmin;
        UserName = userName;
    }

    public bool IsAdmin { get; protected set; }
    public List<UserOrganizationRecord> UserOrganizations { get; set; } = [];

    public void MakeUserAdmin()
    {
        IsAdmin = true;
    }
}