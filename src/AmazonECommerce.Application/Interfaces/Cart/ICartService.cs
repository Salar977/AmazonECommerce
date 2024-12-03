using AmazonECommerce.Application.DTOs;
using AmazonECommerce.Application.DTOs.Cart;

namespace AmazonECommerce.Application.Interfaces.Cart;

public interface ICartService
{
    Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<OrderRequest> orders);
    Task<ServiceResponse> Checkout(Checkout checkout);
}