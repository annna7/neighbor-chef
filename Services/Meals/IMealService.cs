using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;

namespace neighbor_chef.Services;

public interface IMealService
{
    Task<IEnumerable<Meal>> GetAllMealsAsync();
    Task<Meal> CreateMealAsync(CreateMealDto createMealDto, Guid chefId);
    Task<Meal?> GetMealAsync(Guid id);
    Task<Meal> UpdateMealAsync(Guid id, UpdateMealDto updateMealDto);
    Task<Meal> AddIngredientAsync(Guid id, AddIngredientDto addIngredientDto);
    Task<Meal> RemoveIngredientAsync(Guid id, RemoveIngredientDto removeIngredientDto);
    Task<Meal> DeleteMealAsync(Guid id);
}