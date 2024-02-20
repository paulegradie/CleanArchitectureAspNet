using System.ComponentModel.DataAnnotations;
using Persistence.EntityFramework;

namespace Persistence.Tables;

[DatabaseModel]
public class OrganizationRecord
{
#pragma warning disable CS8618
    public OrganizationRecord()
#pragma warning restore CS8618
    {
        
    }
    public OrganizationRecord(string name)
    {
        Name = name;
        UserOrganizations = new List<UserOrganizationRecord>();
    }

    [Key]
    public int OrganizationId { get; set; }
    public string Name { get; set; }
    public List<UserOrganizationRecord> UserOrganizations { get; set; }

    public void AddUserOrganization(UserOrganizationRecord userOrganizationRecord)
    {
        UserOrganizations.Add(userOrganizationRecord);
    }
}