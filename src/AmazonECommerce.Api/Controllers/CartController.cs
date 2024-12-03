using AmazonECommerce.Application.DTOs.Cart;
using AmazonECommerce.Application.Interfaces.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazonECommerce.Api.Controllers;
[Route("api/cart")]
[ApiController]
public class CartController(ICartService cartService) : ControllerBase
{
    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout(Checkout checkout)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        var result = await cartService.Checkout(checkout);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("save-checkout")]
    public async Task<IActionResult> SaveCheckout(IEnumerable<OrderRequest> orderRequest)
    {
        var result = await cartService.SaveCheckoutHistory(orderRequest);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
