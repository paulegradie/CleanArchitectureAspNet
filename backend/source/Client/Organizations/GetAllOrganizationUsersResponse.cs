namespace Client.Organizations;

public record OrganizationUser(string UserName);

public record Organization(string Name, IEnumerable<OrganizationUser> Members);

public record GetAllOrganizationUsersResponse(IEnumerable<Organization> Organizations);