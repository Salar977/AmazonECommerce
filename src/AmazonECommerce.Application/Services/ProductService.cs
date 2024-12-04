using AmazonECommerce.Application.DTOs;
using AmazonECommerce.Application.DTOs.Products;
using AmazonECommerce.Application.Interfaces;
using AmazonECommerce.Application.Interfaces.Categories;
using AmazonECommerce.Application.Interfaces.Products;
using AmazonECommerce.Domain.Entities;
using AutoMapper;

namespace AmazonECommerce.Application.Services;

public class ProductService(IProductRepository productRepository,
                            ICategoryRepository categoryRepository,
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
        var products = await productRepository.GetAllAsync();
        if (!products.Any()) return [];

        var mappedData = mapper.Map<IEnumerable<ProductResponse>>(products);
        foreach (var product in mappedData)
        {
            var category = await categoryRepository.GetByIdAsync(product.CategoryId);
            product.CategoryName = category.Name;
        }

        return mappedData;
        
    }

    public async Task<ProductResponse> GetByIdAsync(Guid id)
    {
        var entity = await productRepository.GetByIdAsync(id);
        if (entity is null) return new ProductResponse();

        var category = await categoryRepository.GetByIdAsync(entity.CategoryId);

        var product = mapper.Map<ProductResponse>(entity);

        product.CategoryName = category.Name;

        return product;
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
