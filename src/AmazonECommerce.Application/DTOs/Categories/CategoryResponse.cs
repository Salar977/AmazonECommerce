using AmazonECommerce.Application.DTOs.Products;

namespace AmazonECommerce.Application.DTOs.Categories;

public class CategoryResponse : BaseCategory
{
    public Guid Id { get; set; }
    public IEnumerable<ProductResponse>? Products { get; set; }
}