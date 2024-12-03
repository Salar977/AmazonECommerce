using AmazonECommerce.Application.DTOs.Cart;

namespace AmazonECommerce.Application.Interfaces.Cart;

public interface IPaymentMethodService
{
    Task<IEnumerable<PaymentMethodResponse>> GetPaymentMethodsAsync();
}