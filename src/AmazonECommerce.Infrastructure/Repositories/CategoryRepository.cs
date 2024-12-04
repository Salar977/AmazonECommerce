using AmazonECommerce.Application.Interfaces.Categories;
using AmazonECommerce.Domain.Entities;
using AmazonECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AmazonECommerce.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext dbContext) : ICategoryRepository
{
    public async Task<int> AddAsync(Category category)
    {
        await dbContext.Categories.AddAsync(category);
        return await dbContext.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var category = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category is null) return 0;
        dbContext.Categories.Remove(category);
        return await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await dbContext.Categories.Include(x => x.Products).AsNoTracking().ToListAsync();
    }

    public async Task<Category> GetByIdAsync(Guid id)
    {
        return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id)
            ?? null!;
    }

    public async Task<int> UpdateAsync(Guid id, Category category)
    {
        var updateCategory = await GetByIdAsync(id);
        if (updateCategory is null) return 0;

        updateCategory.Name = string.IsNullOrEmpty(category.Name) ? updateCategory.Name : category.Name;

        return await dbContext.SaveChangesAsync();
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
}