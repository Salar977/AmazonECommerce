using AmazonECommerce.Application.Interfaces.Category;
using AmazonECommerce.Domain.Entities;
using AmazonECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AmazonECommerce.Infrastructure.Repositories.Category;

public class CategoryRepository(AppDbContext dbContext) : ICategoryRepository
{
    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId)
    {
        var products = await dbContext.Products
            .Include(x => x.Category)
            .Where(x => x.CategoryId == categoryId)
            .AsNoTracking()
            .ToListAsync();

        return products.Count > 0 ? products : [];
    }
}