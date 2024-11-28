using AmazonECommerce.Application.DTOs.Products;
using AmazonECommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AmazonECommerce.Api.Controllers;
[Route("api/products")]
[ApiController]
public class ProductController(ProductService productService) : ControllerBase
{
    [HttpGet(Name = "GetProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        var data = await productService.GetAllAsync();
        return data.Count() > 0 ? Ok(data) : NotFound(data);
    }

    [HttpGet("{id:guid}", Name = "GetProduct")]
    public async Task<IActionResult> GetProduct([FromQuery] Guid id)
    {
        var product = await productService.GetByIdAsync(id);
        if(product is null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost(Name = "AddProduct")]
    public async Task<IActionResult> AddProduct([FromBody] ProductRequest productRequest)
    {
        var result = await productService.AddAsync(productRequest);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPut("{id:guid}", Name = "UpdateProduct")]
    public async Task<IActionResult> UpdateProduct([FromQuery] Guid id,
                                                   [FromBody] ProductUpdate productUpdate)
    {
        var result = await productService.UpdateAsync(id, productUpdate);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpDelete("{id:guid}", Name = "DeleteProduct")]
    public async Task<IActionResult> UpdateProduct([FromQuery] Guid id)
    {
        var result = await productService.DeleteAsync(id);
        if(result is null) return NotFound(result);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}
