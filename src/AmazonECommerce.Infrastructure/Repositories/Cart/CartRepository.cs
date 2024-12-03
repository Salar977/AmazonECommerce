using AmazonECommerce.Application.Interfaces.Cart;
using AmazonECommerce.Domain.Entities.Cart;
using AmazonECommerce.Infrastructure.Data;

namespace AmazonECommerce.Infrastructure.Repositories.Cart;

public class CartRepository(AppDbContext dbContext) : ICartRepository
{
    public async Task<int> SaveCheckoutHistory(IEnumerable<Order> checkouts)
    {
        dbContext.Orders.AddRange(checkouts);
        return await dbContext.SaveChangesAsync();
    }
}