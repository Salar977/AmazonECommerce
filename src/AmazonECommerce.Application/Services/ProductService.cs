using AmazonECommerce.Application.DTOs;
using AmazonECommerce.Application.DTOs.Products;
using AmazonECommerce.Application.Interfaces;
using AmazonECommerce.Domain.Entities;

namespace AmazonECommerce.Application.Services;

public class ProductService(IGenericRepository<Product> repository) : IProductService
{
    public async Task<ServiceResponse> AddAsync(CreateProduct createProduct)
    {

        int result = await repository.AddAsync();
    }

    public Task<ServiceResponse> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProductResponse> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> UpdateAsync(Guid id, UpdateProduct updateProduct)
    {
        throw new NotImplementedException();
    }
}
