using AmazonECommerce.Application.DTOs;
using AmazonECommerce.Application.DTOs.Cart;
using AmazonECommerce.Application.Interfaces.Cart;
using Stripe;
using Stripe.Checkout;

namespace AmazonECommerce.Infrastructure.Services;

public class PaymentService : IPaymentService
{
    public async Task<ServiceResponse> Pay(decimal totalAmount, IEnumerable<Domain.Entities.Product> cartProducts, IEnumerable<ProcessCart> carts)
    {
        try
        {
            var lineItems = new List<SessionLineItemOptions>();
        
            foreach(var item in cartProducts)
            {
                var pQuantity = carts.FirstOrDefault(_ => _.ProductId == item.Id);
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name,
                            Description = item.Description,
                        },
                        UnitAmount = (long)(item.Price * 100)
                    },
                    Quantity = pQuantity!.Quantity
                });
            }
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = ["card"],
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://localhost:7188/payment-success",
                CancelUrl = "https://localhost:7188/payment-cancel",
            };

        
            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            return new ServiceResponse(true, session.Url);
        }
        catch(StripeException e)
        {
            return new ServiceResponse(Message: e.Message);
        }
    }
}