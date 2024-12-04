using AmazonECommerce.Domain.Entities;

namespace AmazonECommerce.Application.Interfaces.Categories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category> GetByIdAsync(Guid id);
    Task<int> AddAsync(Category category);
    Task<int> UpdateAsync(Guid id, Category category);
    Task<int> DeleteAsync(Guid id);


    Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId);
}