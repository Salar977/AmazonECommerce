using AmazonECommerce.Application.DTOs;
using AmazonECommerce.Application.DTOs.Categories;

namespace AmazonECommerce.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetAllAsync();
    Task<CategoryResponse> GetByIdAsync(Guid id);
    Task<ServiceResponse> AddAsync(CategoryRequest createCategory);
    Task<ServiceResponse> UpdateAsync(Guid id, CategoryUpdate updateCategory);
    Task<ServiceResponse> DeleteAsync(Guid id);
}