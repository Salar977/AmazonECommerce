using AmazonECommerce.Domain.Entities.Cart;

namespace AmazonECommerce.Application.Interfaces.Cart;

public interface IPaymentMethodRepository
{
    Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync();
}