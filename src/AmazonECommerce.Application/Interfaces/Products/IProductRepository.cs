using AmazonECommerce.Domain.Entities;

namespace AmazonECommerce.Application.Interfaces.Products;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(Guid id);
    Task<int> AddAsync(Product product);
    Task<int> UpdateAsync(Guid id, Product product);
    Task<int> DeleteAsync(Guid id);
}