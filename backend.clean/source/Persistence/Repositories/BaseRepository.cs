using Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext context;
    protected readonly DbSet<TEntity> dbSet;

    public BaseRepository(AppDbContext context)
    {
        this.context = context;
        dbSet = this.context.Set<TEntity>();
    }

    public async Task<TEntity> GetByIdAsync(object id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await dbSet.AddAsync(entity);
    }

    public void Remove(TEntity entity)
    {
        dbSet.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        dbSet.Update(entity);
    }
}