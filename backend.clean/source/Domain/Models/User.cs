using Domain.Abstractions;

namespace Domain.Models;

public record User(string Name, bool IsAdmin) : IDomainModel;