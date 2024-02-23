namespace Domain.Abstractions.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(object id);
    Task<List<TEntity>> GetAllAsync();
    Task AddAsync(TEntity entity);
    void Remove(TEntity entity);
    void Update(TEntity entity);
}