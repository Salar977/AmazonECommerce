using AmazonECommerce.Application.DTOs;
using AmazonECommerce.Application.DTOs.Categories;
using AmazonECommerce.Application.DTOs.Products;
using AmazonECommerce.Application.Interfaces;
using AmazonECommerce.Domain.Entities;
using AutoMapper;

namespace AmazonECommerce.Application.Services;

public class CategoryService(IGenericRepository<Category> repository, IMapper mapper) : ICategoryService
{
    public async Task<ServiceResponse> AddAsync(CategoryRequest createCategory)
    {
        var mappedData = mapper.Map<Category>(createCategory);
        int result = await repository.AddAsync(mappedData);

        return result <= 0 ?
            new ServiceResponse(Message: "Category Failed to be added") :
            new ServiceResponse(true, Message: "Category Added.");
    }

    public async Task<ServiceResponse> DeleteAsync(Guid id)
    {
        int result = await repository.DeleteAsync(id);
        return result <= 0 ?
            new ServiceResponse(Message: "Category not found or failed to be deleted") :
            new ServiceResponse(true, Message: "Category deleted.");
    }

    public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
    {
        var category = await repository.GetAllAsync();
        if (!category.Any()) return [];

        return mapper.Map<IEnumerable<CategoryResponse>>(category);
    }

    public async Task<CategoryResponse> GetByIdAsync(Guid id)
    {
        var category = await repository.GetByIdAsync(id);
        if (category is null) return new CategoryResponse();

        return mapper.Map<CategoryResponse>(category);
    }

    public async Task<ServiceResponse> UpdateAsync(Guid id, CategoryUpdate updateCategory)
    {
        var category = await repository.GetByIdAsync(id);
        if (category is null)
            return new ServiceResponse(Message: "Category failed to update");

        var mappedData = mapper.Map<Category>(updateCategory);
        int result = await repository.UpdateAsync(id, mappedData);

        return result <= 0 ?
            new ServiceResponse(Message: "Category Failed to be updated") :
            new ServiceResponse(true, Message: "Category Updated.");
    }
}