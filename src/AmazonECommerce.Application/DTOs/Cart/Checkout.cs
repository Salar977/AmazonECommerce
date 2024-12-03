namespace AmazonECommerce.Application.DTOs.Cart;

public class Checkout
{
    public required Guid PaymentMethodId { get; set; }
    public required IEnumerable<ProcessCart> Carts { get; set; }
}