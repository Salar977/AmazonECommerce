using System.ComponentModel.DataAnnotations;

namespace AmazonECommerce.Application.DTOs.Products;

public class ProductUpdate
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
