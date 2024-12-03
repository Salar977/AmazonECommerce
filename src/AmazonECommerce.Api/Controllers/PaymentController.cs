using AmazonECommerce.Application.DTOs.Cart;
using AmazonECommerce.Application.Interfaces.Cart;
using Microsoft.AspNetCore.Mvc;

namespace AmazonECommerce.Api.Controllers;
[Route("api/payments")]
[ApiController]
public class PaymentController(IPaymentMethodService paymentMethodService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaymentMethodResponse>>> GetPaymentMethods()
    {
        var paymentMethods = await paymentMethodService.GetPaymentMethodsAsync();
        if(!paymentMethods.Any()) return NotFound();

        return Ok(paymentMethods);
    }
}
