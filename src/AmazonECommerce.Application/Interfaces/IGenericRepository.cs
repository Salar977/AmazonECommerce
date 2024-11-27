namespace AmazonECommerce.Application.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(Guid id);
    Task<int> AddAsync(TEntity entity);
    Task<int> UpdateAsync(Guid id, TEntity entity);
    Task<int> DeleteAsync(Guid id);
}