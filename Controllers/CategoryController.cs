using Microsoft.AspNetCore.Mvc;
using neighbor_chef.Models.DTOs;
using neighbor_chef.Services;

namespace neighbor_chef.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryDto categoryDto)
    {
        var category = await _categoryService.CreateCategoryAsync(categoryDto.Name);
        return Ok(category);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var category = await _categoryService.GetCategoryAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }
    
    [HttpGet("getByName/{name}")]
    public async Task<IActionResult> GetCategoryByName(string name)
    {
        var category = await _categoryService.GetCategoryAsync(name);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }
    

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }
}