using Domain.Abstractions;
using Domain.Exceptions;
using Domain.Models;
using Persistence.Tables;

namespace Persistence.ToDomainModelMappers;

public class ToUserDomainModelMapper : IMapToTheDomain<ApplicationUserRecord, User>
{
    public Task<User> Map(ApplicationUserRecord from, CancellationToken cancellationToken = default)
    {
        if (from.UserName is null) throw new DomainException("Failed retrieve username from user record");
        return Task.FromResult(new User(from.UserName, from.IsAdmin));
    }
}