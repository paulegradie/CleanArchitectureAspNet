using Domain.Abstractions;

namespace Domain.Models;

public record UserOrganization(User User, Organization Organization) : IDomainModel;