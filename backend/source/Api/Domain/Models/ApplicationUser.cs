using Microsoft.AspNetCore.Identity;

namespace Api.Domain.Models;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
    }

    public ApplicationUser(bool isAdmin, string userName)
    {
        IsAdmin = isAdmin;
        UserName = userName;
    }

    public bool IsAdmin { get; set; }
    public List<UserOrganization> UserOrganizations { get; set; } = new();

    public void MakeUserAdmin()
    {
        IsAdmin = true;
    }
}