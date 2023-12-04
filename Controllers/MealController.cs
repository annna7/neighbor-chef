using System.Security.Claims;
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
    private readonly IPersonService _peopleService;
    private readonly IChefService _chefService;

    public MealController(IChefService chefService, IMealService mealService, UserManager<ApplicationUser> userManager, IPersonService peopleService)
    {
        _mealService = mealService;
        _userManager = userManager;
        _peopleService = peopleService;
        _chefService = chefService;
    }
    

    [HttpPost]
    [Authorize(Roles = "Chef", AuthenticationSchemes = "Bearer")] // Only chefs can create meals
    public async Task<IActionResult> CreateMeal([FromBody] CreateMealDto createMealDto)
    {
        var chefEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (chefEmail == null) return Unauthorized("You are not authorized to create a meal, please log in as a chef.");
        var chef = await _peopleService.GetPersonAsync(chefEmail);
        if (chef == null) return Unauthorized("You are not authorized to create a meal, please log in as a chef.");
        var meal = await _mealService.CreateMealAsync(createMealDto, chef.Id);
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
    [Authorize(Roles = "Chef", AuthenticationSchemes = "Bearer")] // Only the owning chef can update the meal
    public async Task<IActionResult> UpdateMeal(Guid id, [FromBody] UpdateMealDto updateMealDto)
    {
        var chefEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (chefEmail == null) return Unauthorized("You are not authorized to create a meal, please log in as a chef.");
        
        var meal = await _mealService.GetMealAsync(id);
        if (meal == null) return NotFound("Meal not found.");
        
        var chef = await _peopleService.GetPersonAsync(chefEmail);
        if (chef == null || chef.Id != meal.ChefId) return Forbid("You can only update your own meals.");
        
        return Ok(await _mealService.UpdateMealAsync(id, updateMealDto));
    }
    
    [HttpPost("{id:guid}/ingredients")]
    [Authorize(Roles = "Chef", AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> AddIngredientToMeal(Guid id, [FromBody] AddIngredientDto addIngredientDto)
    {
        var chefEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (chefEmail == null) return Unauthorized("You are not authorized to add ingredients, please log in as a chef.");
    
        var meal = await _mealService.GetMealAsync(id);
        if (meal == null) return NotFound("Meal not found.");
    
        var chef = await _peopleService.GetPersonAsync(chefEmail);
        if (chef == null || chef.Id != meal.ChefId) return Forbid("You can only add ingredients to your own meals.");
    
        meal = await _mealService.AddIngredientAsync(id, addIngredientDto);
    
        return Ok(meal);
    }
    
    [HttpDelete("{id:guid}/ingredients")]
    [Authorize(Roles = "Chef", AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> RemoveIngredientFromMeal(Guid id, [FromBody] RemoveIngredientDto removeIngredientDto)
    {
        var chefEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (chefEmail == null) return Unauthorized("You are not authorized to remove ingredients, please log in as a chef.");
    
        var meal = await _mealService.GetMealAsync(id);
        if (meal == null) return NotFound("Meal not found.");
    
        var chef = await _peopleService.GetPersonAsync(chefEmail);
        if (chef == null || chef.Id != meal.ChefId) return Forbid("You can only remove ingredients from your own meals.");
    
        meal = await _mealService.RemoveIngredientAsync(id, removeIngredientDto);
    
        return Ok(meal);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Chef", AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeleteMeal(Guid id)
    {
        var chefEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (chefEmail == null) return Unauthorized("You are not authorized to delete meals, please log in as a chef.");
        
        var meal = await _mealService.GetMealAsync(id);
        if (meal == null) return NotFound("Meal not found.");
        
        var chef = await _peopleService.GetPersonAsync(chefEmail);
        if (chef == null || chef.Id != meal.ChefId) return Forbid("You can only delete your own meals.");
        
        await _mealService.DeleteMealAsync(id);

        return NoContent();
    }
    
    [HttpGet("chefs")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetChefs()
    {
        var chefs = await _chefService.GetChefsSortedAsync();
        return Ok(chefs);
    }
}
