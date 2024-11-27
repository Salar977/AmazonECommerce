using AmazonECommerce.Application.DTOs;
using AmazonECommerce.Application.DTOs.Products;

namespace AmazonECommerce.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetAllAsync();
    Task<ProductResponse> GetByIdAsync(Guid id);
    Task<ServiceResponse> AddAsync(CreateProduct createProduct);
    Task<ServiceResponse> UpdateAsync(Guid id, UpdateProduct updateProduct);
    Task<ServiceResponse> DeleteAsync(Guid id);
}
