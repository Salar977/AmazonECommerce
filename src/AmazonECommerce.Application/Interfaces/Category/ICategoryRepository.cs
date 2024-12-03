using AmazonECommerce.Domain.Entities;

namespace AmazonECommerce.Application.Interfaces.Category;

public interface ICategoryRepository
{
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId);

}