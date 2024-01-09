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
    private readonly IChefService _chefService;
    private readonly IAccountService _accountService;
    
    public MealController(IChefService chefService, IMealService mealService, IAccountService accountService)
    {
        _mealService = mealService;
        _accountService = accountService;
        _chefService = chefService;
    }
    

    [HttpGet("all")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetAllMeals()
    {
        var chefEmail = _accountService.GetEmailFromToken(Request.Headers["Authorization"].ToString().Split(" ")[1]);
        var chef = await _chefService.GetPersonAsync(chefEmail, true);
        if (chef == null) return Unauthorized("You are not authorized to view meals, please log in as a chef.");
        var meals = await _mealService.GetAllMealsAsync();
        return Ok(meals);
    }
    
    [HttpPost]
    [Authorize(Roles = "Chef", AuthenticationSchemes = "Bearer")] // Only chefs can create meals
    public async Task<IActionResult> CreateMeal([FromBody] CreateMealDto createMealDto)
    {
        var chefEmail = _accountService.GetEmailFromToken(Request.Headers["Authorization"].ToString().Split(" ")[1]);
        var chef = await _chefService.GetPersonAsync(chefEmail, true);
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
        var chefEmail = _accountService.GetEmailFromToken(Request.Headers["Authorization"].ToString().Split(" ")[1]);
        
        var meal = await _mealService.GetMealAsync(id);
        if (meal == null) return NotFound("Meal not found.");
        
        var chef = await _chefService.GetPersonAsync(chefEmail, true);
        if (chef == null || chef.Id != meal.ChefId) return Forbid("You can only update your own meals.");
        
        return Ok(await _mealService.UpdateMealAsync(id, updateMealDto));
    }
    
    [HttpPost("{id:guid}/ingredients")]
    [Authorize(Roles = "Chef", AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> AddIngredientToMeal(Guid id, [FromBody] AddIngredientDto addIngredientDto)
    {
        var chefEmail = _accountService.GetEmailFromToken(Request.Headers["Authorization"].ToString().Split(" ")[1]);

        var meal = await _mealService.GetMealAsync(id);
        if (meal == null) return NotFound("Meal not found.");
    
        var chef = await _chefService.GetPersonAsync(chefEmail, true);
        if (chef == null || chef.Id != meal.ChefId) return Forbid("You can only add ingredients to your own meals.");
    
        meal = await _mealService.AddIngredientAsync(id, addIngredientDto);
    
        return Ok(meal);
    }
    
    [HttpDelete("{id:guid}/ingredients")]
    [Authorize(Roles = "Chef", AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> RemoveIngredientFromMeal(Guid id, [FromBody] RemoveIngredientDto removeIngredientDto)
    {
        var chefEmail = _accountService.GetEmailFromToken(Request.Headers["Authorization"].ToString().Split(" ")[1]);

        var meal = await _mealService.GetMealAsync(id);
        if (meal == null) return NotFound("Meal not found.");
    
        var chef = await _chefService.GetPersonAsync(chefEmail, true);
        if (chef == null || chef.Id != meal.ChefId) return Forbid("You can only remove ingredients from your own meals.");
    
        meal = await _mealService.RemoveIngredientAsync(id, removeIngredientDto);
    
        return Ok(meal);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Chef", AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeleteMeal(Guid id)
    {
        var chefEmail = _accountService.GetEmailFromToken(Request.Headers["Authorization"].ToString().Split(" ")[1]);

        var meal = await _mealService.GetMealAsync(id);
        if (meal == null) return NotFound("Meal not found.");
        
        var chef = await _chefService.GetPersonAsync(chefEmail, true);
        if (chef == null || chef.Id != meal.ChefId) return Forbid("You can only delete your own meals.");
        
        await _mealService.DeleteMealAsync(id);

        return NoContent();
    }
}
