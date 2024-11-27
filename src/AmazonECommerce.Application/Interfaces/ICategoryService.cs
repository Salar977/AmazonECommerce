using AmazonECommerce.Application.DTOs;
using AmazonECommerce.Application.DTOs.Categories;

namespace AmazonECommerce.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetAllAsync();
    Task<CategoryResponse> GetByIdAsync(Guid id);
    Task<ServiceResponse> AddAsync(CreateCategory createCategory);
    Task<ServiceResponse> UpdateAsync(Guid id, UpdateCategory updateCategory);
    Task<ServiceResponse> DeleteAsync(Guid id);
}