using AmazonECommerce.Application.Interfaces.Categories;
using AmazonECommerce.Domain.Entities;
using AmazonECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AmazonECommerce.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext dbContext) : ICategoryRepository
{
    public Task<int> AddAsync(Category category)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Category>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Category> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId)
    {
        var products = await dbContext.Products
            .Include(x => x.Category)
            .Where(x => x.CategoryId == categoryId)
            .AsNoTracking()
            .ToListAsync();

        return products.Count > 0 ? products : [];
    }

    public Task<int> UpdateAsync(Guid id, Category category)
    {
        throw new NotImplementedException();
    }
}