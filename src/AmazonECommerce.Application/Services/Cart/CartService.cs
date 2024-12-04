using AmazonECommerce.Application.DTOs;
using AmazonECommerce.Application.DTOs.Cart;
using AmazonECommerce.Application.Interfaces;
using AmazonECommerce.Application.Interfaces.Cart;
using AmazonECommerce.Application.Interfaces.Products;
using AmazonECommerce.Domain.Entities;
using AmazonECommerce.Domain.Entities.Cart;
using AutoMapper;

namespace AmazonECommerce.Application.Services.Cart;

public class CartService(ICartRepository cartRepository,
                         IMapper mapper,
                         IProductRepository repository,
                         IPaymentMethodService paymentMethodService,
                         IPaymentService paymentService) : ICartService
{
    public async Task<ServiceResponse> Checkout(Checkout checkout)
    {
        var (products, totalAmount) = await GetTotalAmount(checkout.Carts);
        var paymentMethods = await paymentMethodService.GetPaymentMethodsAsync();

        if(checkout.PaymentMethodId == paymentMethods.FirstOrDefault()!.Id)
            return await paymentService.Pay(totalAmount, products, checkout.Carts);

        return new ServiceResponse(Message: "Invalid payment method.");
    }

    public async Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<OrderRequest> orders)
    {
        var mappedData = mapper.Map<IEnumerable<Order>>(orders);
        var result = await cartRepository.SaveCheckoutHistory(mappedData);

        return result > 0 ?
            new ServiceResponse(true, "Order Stored.") :
            new ServiceResponse(Message: "Error occurred in saving order");
    }

    private async Task<(IEnumerable<Product>, decimal)> GetTotalAmount(IEnumerable<ProcessCart> processCarts)
    {
        if(!processCarts.Any()) return ([], 0);

        var products = await repository.GetAllAsync();
        if (products.Any()) return ([], 0);

        var cartProducts = processCarts
            .Select(cartItem => products.FirstOrDefault(p => p.Id == cartItem.ProductId))
            .Where(product => product != null)
            .ToList();

        var totalAmount = processCarts
            .Where(cartItem => cartProducts.Any(p => p!.Id == cartItem.ProductId))
            .Sum(cartItem => cartItem.Quantity * cartProducts.First(p => p!.Id == cartItem.ProductId)!.Price);

        return (cartProducts!, totalAmount);
    }
}