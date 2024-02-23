using Domain.Abstractions.Repositories;

namespace Persistence.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    // Add any other unit of work specific methods here.
}