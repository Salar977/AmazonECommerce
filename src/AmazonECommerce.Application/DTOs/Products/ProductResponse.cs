using AmazonECommerce.Application.DTOs.Categories;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AmazonECommerce.Application.DTOs.Products;

public class ProductResponse
{
    public Guid Id { get; set; }

    [JsonIgnore]
    public CategoryResponse? Category { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime Created {  get; set; }
    public DateTime? Updated { get; set; }
    public Guid CategoryId { get; set; }
}