using AmazonECommerce.Application.DTOs;
using AmazonECommerce.Application.DTOs.Cart;
using AmazonECommerce.Domain.Entities;

namespace AmazonECommerce.Application.Interfaces.Cart;

public interface IPaymentService
{
    Task<ServiceResponse> Pay(decimal totalAmount, IEnumerable<Product> cartProducts, IEnumerable<ProcessCart> carts);
}