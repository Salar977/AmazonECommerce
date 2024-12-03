using AmazonECommerce.Domain.Entities.Cart;

namespace AmazonECommerce.Application.Interfaces.Cart;

public interface ICartRepository
{
    Task<int> SaveCheckoutHistory(IEnumerable<Order> checkouts);
}