using AmazonECommerce.Application.DTOs.Cart;
using AmazonECommerce.Application.Interfaces.Cart;
using AutoMapper;

namespace AmazonECommerce.Application.Services.Cart;

public class PaymentMethodService(IPaymentMethodRepository paymentMethodRepository,
                                  IMapper mapper) : IPaymentMethodService
{
    public async Task<IEnumerable<PaymentMethodResponse>> GetPaymentMethodsAsync()
    {
        var paymentMethods = await paymentMethodRepository.GetPaymentMethodsAsync();
        if (!paymentMethods.Any()) return [];

        return mapper.Map<IEnumerable<PaymentMethodResponse>>(paymentMethods);

    }
}