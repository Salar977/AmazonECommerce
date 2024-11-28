using AmazonECommerce.Application.DTOs;
using AmazonECommerce.Application.DTOs.Products;
using AmazonECommerce.Application.Interfaces;
using AmazonECommerce.Domain.Entities;
using AutoMapper;

namespace AmazonECommerce.Application.Services;

public class ProductService(IGenericRepository<Product> repository, IMapper mapper) : IProductService
{
    public async Task<ServiceResponse> AddAsync(ProductRequest createProduct)
    {
        var mappedData = mapper.Map<Product>(createProduct);
        int result = await repository.AddAsync(mappedData);

        return result <= 0 ?
            new ServiceResponse(Message: "Product Failed to be added") :
            new ServiceResponse(true, Message: "Product Added.");
    }

    public async Task<ServiceResponse> DeleteAsync(Guid id)
    {
         int result = await repository.DeleteAsync(id);
        return result <= 0 ?
            new ServiceResponse(Message: "Product not found or failed to be deleted") :
            new ServiceResponse(true, Message: "Product deleted.");
    }

    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        var product = await repository.GetAllAsync();
        if (!product.Any()) return [];

        return mapper.Map<IEnumerable<ProductResponse>>(product);
        
    }

    public async Task<ProductResponse> GetByIdAsync(Guid id)
    {
        var product = await repository.GetByIdAsync(id);
        if (product is null) return new ProductResponse();

        return mapper.Map<ProductResponse>(product);
    }

    public async Task<ServiceResponse> UpdateAsync(Guid id, ProductUpdate updateProduct)
    {
        var product = await repository.GetByIdAsync(id);
        if (product is null)
            return new ServiceResponse(Message: "Product failed to update");

        var mappedData = mapper.Map<Product>(updateProduct);
        int result = await repository.UpdateAsync(id, mappedData);

        return result <= 0 ?
            new ServiceResponse(Message: "Product Failed to be updated") :
            new ServiceResponse(true, Message: "Product Updated.");
    }
}
