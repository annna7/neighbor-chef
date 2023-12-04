using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;
using neighbor_chef.Services;

namespace neighbor_chef.Controllers;

[ApiController]
[Route("[controller]")]
public class MealController : ControllerBase
{
    private readonly IMealService _mealService;
    private readonly UserManager<ApplicationUser> _userManager;

    public MealController(IMealService mealService, UserManager<ApplicationUser> userManager)
    {
        _mealService = mealService;
        _userManager = userManager;
    }
    
    [HttpGet]
    [Authorize(Roles = "Chef")] // Only chefs can create meals

    public Task<IActionResult> Hello()
    {
        return Task.FromResult<IActionResult>(Ok("hello"));
    }

    [HttpPost]
    [Authorize(Roles = "Chef")] // Only chefs can create meals
    public async Task<IActionResult> CreateMeal([FromBody] CreateMealDto createMealDto)
    {
        var chef = _userManager.GetUserId(User);
        if (chef == null) return Unauthorized("You are not authorized to create a meal.");
        var chefId = Guid.Parse(chef);
        var meal = await _mealService.CreateMealAsync(createMealDto, chefId);
        return Ok(meal);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMealById(Guid id)
    {
        var meal = await _mealService.GetMealAsync(id);
        if (meal == null)
        {
            return NotFound();
        }
        return Ok(meal);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Chef")] // Only the owning chef can update the meal
    public async Task<IActionResult> UpdateMeal(Guid id, [FromBody] CreateMealDto updateMealDto)
    {
        var chef = _userManager.GetUserId(User);
        if (chef == null) return Unauthorized("You are not authorized to update this meal.");
        var chefId = Guid.Parse(chef);
        
        var meal = await _mealService.GetMealAsync(id);

        if (meal == null) return NotFound("Meal not found.");
        if (meal.ChefId != chefId) return Forbid("You are not allowed to update this meal.");

        await _mealService.UpdateMealAsync(id, updateMealDto);
        return NoContent();
    }
}
