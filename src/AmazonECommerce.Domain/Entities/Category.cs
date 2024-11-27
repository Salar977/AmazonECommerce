using System.ComponentModel.DataAnnotations;

namespace AmazonECommerce.Domain.Entities;

public class Category
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public IEnumerable<Product> Products { get; set; } = new List<Product>();
}