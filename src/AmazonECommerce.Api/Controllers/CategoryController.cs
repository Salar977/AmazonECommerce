using AmazonECommerce.Application.DTOs.Categories;
using AmazonECommerce.Application.Interfaces.Categories;
using Microsoft.AspNetCore.Mvc;

namespace AmazonECommerce.Api.Controllers;
[Route("api/categories")]
[ApiController]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet(Name = "GetCategories")]
    public async Task<IActionResult> GetAllCategories()
    {
        var data = await categoryService.GetAllAsync();
        return data.Count() > 0 ? Ok(data) : NotFound(data);
    }

    [HttpGet("{id:guid}", Name = "GetCategory")]
    public async Task<IActionResult> GetCategory([FromRoute] Guid id)
    {
        var category = await categoryService.GetByIdAsync(id);
        if (category is null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost(Name = "AddCategory")]
    public async Task<IActionResult> AddCategory([FromBody] CategoryRequest categoryRequest)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await categoryService.AddAsync(categoryRequest);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPut("{id:guid}", Name = "UpdateCategory")]
    public async Task<IActionResult> UpdateCategory([FromRoute] Guid id,
                                                   [FromBody] CategoryUpdate categoryUpdate)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await categoryService.UpdateAsync(id, categoryUpdate);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpDelete("{id:guid}", Name = "DeleteCategory")]
    public async Task<IActionResult> UpdateCategory([FromRoute] Guid id)
    {
        var result = await categoryService.DeleteAsync(id);
        if (result is null) return NotFound(result);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    [HttpGet("products/{categoryId:guid}")]
    public async Task<IActionResult> GetProductsByCategoryId([FromRoute] Guid categoryId)
    {
        var products = await categoryService.GetProductByCategoryAsync(categoryId);
        if (products is null || !products.Any()) return NotFound(products);
        return Ok(products);
    }
}
