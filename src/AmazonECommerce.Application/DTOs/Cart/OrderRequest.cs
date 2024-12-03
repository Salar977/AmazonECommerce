namespace AmazonECommerce.Application.DTOs.Cart;

public class OrderRequest
{
    public required Guid ProductId { get; set; }
    public required int Quantity { get; set; }
    public required Guid UserId { get; set; }
}