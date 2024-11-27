using AmazonECommerce.Application.DTOs;
using AmazonECommerce.Application.DTOs.Categories;
using AmazonECommerce.Application.Interfaces;
using AmazonECommerce.Domain.Entities;

namespace AmazonECommerce.Application.Services;

public class CategoryService(IGenericRepository<Category> repository) : ICategoryService
{
    public Task<ServiceResponse> AddAsync(CreateCategory createCategory)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CategoryResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CategoryResponse> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> UpdateAsync(Guid id, UpdateCategory updateCategory)
    {
        throw new NotImplementedException();
    }
}