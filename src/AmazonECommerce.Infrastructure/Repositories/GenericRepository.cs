using AmazonECommerce.Application.Exceptions;
using AmazonECommerce.Application.Interfaces;
using AmazonECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AmazonECommerce.Infrastructure.Repositories;

public class GenericRepository<TEntity>(AppDbContext dbContext) : IGenericRepository<TEntity> where TEntity : class
{
    public async Task<int> AddAsync(TEntity entity)
    {
        await dbContext.Set<TEntity>().AddAsync(entity);
        return await dbContext.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var entity = await dbContext.Set<TEntity>().FindAsync(id) ??
            throw new ItemNotFoundException($"{typeof(TEntity).Name} does not exist");

        dbContext.Set<TEntity>().Remove(entity);
        return await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await dbContext.Set<TEntity>().FindAsync(id) ??
            throw new ItemNotFoundException($"{typeof(TEntity).Name} does not exist");
    }

    public async Task<int> UpdateAsync(Guid id, TEntity entity)
    {
        var item = await dbContext.Set<TEntity>().FindAsync(id) ??
            throw new ItemNotFoundException($"{typeof(TEntity).Name} does not exist");

        dbContext.Set<TEntity>().Update(item);
        return await dbContext.SaveChangesAsync();
    }
}