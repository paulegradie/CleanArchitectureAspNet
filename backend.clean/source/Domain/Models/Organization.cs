using Domain.Abstractions;

namespace Domain.Models;

public record Organization(string Name, IEnumerable<UserOrganization> UserOrganizations) : IDomainModel;