using AmazonECommerce.Application.DTOs.Categories;

namespace AmazonECommerce.Application.DTOs.Products;

public class ProductResponse : BaseProduct
{
    public Guid Id { get; set; }
    public CategoryResponse? Categories { get; set; }
}