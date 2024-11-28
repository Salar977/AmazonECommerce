using AmazonECommerce.Application.DTOs.Products;

namespace AmazonECommerce.Application.DTOs.Categories;

public class CategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<ProductResponse>? Products { get; set; }
}