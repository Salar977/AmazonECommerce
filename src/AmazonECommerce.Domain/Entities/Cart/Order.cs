namespace AmazonECommerce.Domain.Entities.Cart;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public Guid UserId { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;

}