using AmazonECommerce.Application.DTOs;
using AmazonECommerce.Application.DTOs.Products;
using AmazonECommerce.Application.Interfaces;
using AmazonECommerce.Application.Interfaces.Products;
using AmazonECommerce.Domain.Entities;
using AutoMapper;

namespace AmazonECommerce.Application.Services;

public class ProductService(IProductRepository productRepository,
                            IMapper mapper) : IProductService
{
    public async Task<ServiceResponse> AddAsync(ProductRequest createProduct)
    {
        var mappedData = mapper.Map<Product>(createProduct);
        mappedData.Created = DateTime.Now;
        int result = await productRepository.AddAsync(mappedData);

        return result <= 0 ?
            new ServiceResponse(Message: "Product Failed to be added") :
            new ServiceResponse(true, Message: "Product Added.");
    }

    public async Task<ServiceResponse> DeleteAsync(Guid id)
    {
         int result = await productRepository.DeleteAsync(id);
        return result <= 0 ?
            new ServiceResponse(Message: "Product not found or failed to be deleted") :
            new ServiceResponse(true, Message: "Product deleted.");
    }

    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        var product = await productRepository.GetAllAsync();
        if (!product.Any()) return [];

        return mapper.Map<IEnumerable<ProductResponse>>(product);
        
    }

    public async Task<ProductResponse> GetByIdAsync(Guid id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null) return new ProductResponse();

        return mapper.Map<ProductResponse>(product);
    }

    public async Task<ServiceResponse> UpdateAsync(Guid id, ProductUpdate updateProduct)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null)
            return new ServiceResponse(Message: "Product failed to update");

        var mappedData = mapper.Map<Product>(updateProduct);
        mappedData.Updated = DateTime.Now;
        int result = await productRepository.UpdateAsync(id, mappedData);

        return result <= 0 ?
            new ServiceResponse(Message: "Product Failed to be updated") :
            new ServiceResponse(true, Message: "Product Updated.");
    }
}
