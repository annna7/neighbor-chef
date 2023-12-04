using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;

namespace neighbor_chef.Services;

public interface IMealService
{
    Task<Meal> CreateMealAsync(CreateMealDto createMealDto, Guid chefId);
    Task<Meal?> GetMealAsync(Guid id);
    Task UpdateMealAsync(Guid id, CreateMealDto updateMealDto);
}