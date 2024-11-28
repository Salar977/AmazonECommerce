using AmazonECommerce.Application.DTOs.Categories;
using AmazonECommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AmazonECommerce.Api.Controllers;
[Route("api/categories")]
[ApiController]
public class CategoryController(CategoryService categoryService) : ControllerBase
{
    [HttpGet(Name = "GetCategories")]
    public async Task<IActionResult> GetAllCategories()
    {
        var data = await categoryService.GetAllAsync();
        return data.Count() > 0 ? Ok(data) : NotFound(data);
    }

    [HttpGet("{id:guid}", Name = "GetCategory")]
    public async Task<IActionResult> GetCategory([FromQuery] Guid id)
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
        var result = await categoryService.AddAsync(categoryRequest);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPut("{id:guid}", Name = "UpdateCategory")]
    public async Task<IActionResult> UpdateCategory([FromQuery] Guid id,
                                                   [FromBody] CategoryUpdate categoryUpdate)
    {
        var result = await categoryService.UpdateAsync(id, categoryUpdate);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpDelete("{id:guid}", Name = "DeleteCategory")]
    public async Task<IActionResult> UpdateCategory([FromQuery] Guid id)
    {
        var result = await categoryService.DeleteAsync(id);
        if (result is null) return NotFound(result);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}
