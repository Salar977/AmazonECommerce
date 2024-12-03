using AmazonECommerce.Application.Interfaces.Cart;
using AmazonECommerce.Domain.Entities.Cart;
using AmazonECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AmazonECommerce.Infrastructure.Repositories.Cart;

public class PaymentMethodRepository(AppDbContext dbContext) : IPaymentMethodRepository
{
    public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync()
    {
        return await dbContext.PaymentMethods.AsNoTracking().ToListAsync();
    }
}