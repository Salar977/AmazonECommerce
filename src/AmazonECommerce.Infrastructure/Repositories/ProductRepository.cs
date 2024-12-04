using AmazonECommerce.Application.Interfaces.Products;
using AmazonECommerce.Domain.Entities;
using AmazonECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AmazonECommerce.Infrastructure.Repositories;

public class ProductRepository(AppDbContext dbContext) : IProductRepository
{
    public async Task<int> AddAsync(Product product)
    {
        await dbContext.Products.AddAsync(product);
        return await dbContext.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        if(product is null) return 0;
        dbContext.Products.Remove(product);
        return await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await dbContext.Products.AsNoTracking().ToListAsync();
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        return await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id)
            ?? null!;
    }

    public async Task<int> UpdateAsync(Guid id, Product product)
    {
        var updateProduct = await GetByIdAsync(id);
        if(updateProduct is null) return 0;

        updateProduct.Name = string.IsNullOrEmpty(product.Name) ? updateProduct.Name : product.Name;
        updateProduct.Description = string.IsNullOrEmpty(product.Description) ? updateProduct.Description : product.Description;
        updateProduct.ImageUrl = string.IsNullOrEmpty(product.ImageUrl) ? updateProduct.ImageUrl : product.ImageUrl;
        updateProduct.Price = string.IsNullOrEmpty(product.Price.ToString()) ? updateProduct.Price : product.Price;
        updateProduct.Quantity = string.IsNullOrEmpty(product.Quantity.ToString()) ? updateProduct.Quantity : product.Quantity;
        updateProduct.Updated = product.Updated;

        return await dbContext.SaveChangesAsync();
    }
}