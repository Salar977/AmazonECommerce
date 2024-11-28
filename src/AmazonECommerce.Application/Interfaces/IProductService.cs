using AmazonECommerce.Application.DTOs;
using AmazonECommerce.Application.DTOs.Products;

namespace AmazonECommerce.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetAllAsync();
    Task<ProductResponse> GetByIdAsync(Guid id);
    Task<ServiceResponse> AddAsync(ProductRequest createProduct);
    Task<ServiceResponse> UpdateAsync(Guid id, ProductUpdate updateProduct);
    Task<ServiceResponse> DeleteAsync(Guid id);
}
