namespace AmazonECommerce.Application.DTOs.Cart;

public class ProcessCart
{
    public required Guid ProductId { get; set; }
    public int Quantity { get; set; }
}