using Microsoft.EntityFrameworkCore;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;
using neighbor_chef.UnitOfWork;

namespace neighbor_chef.Services;

public class MealService : IMealService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryService _categoryService;

    public MealService(IUnitOfWork unitOfWork, ICategoryService categoryService)
    {
        _unitOfWork = unitOfWork;
        _categoryService = categoryService;
    }

    public async Task<Meal> CreateMealAsync(CreateMealDto createMealDto, Guid chefId)
    {
        var category = await _categoryService.GetCategoryAsync(createMealDto.CategoryName);
        if (category == null)
        {
            throw new KeyNotFoundException("Category not found.");
        }

        var meal = new Meal
        {
            ChefId = chefId,
            CategoryId = category.Id,
            Name = createMealDto.Name,
            Description = createMealDto.Description,
            PictureUrl = createMealDto.PictureUrl,
            Price = createMealDto.Price,
        };

        await _unitOfWork.GetRepository<Meal>().AddAsync(meal);
        await _unitOfWork.CompleteAsync();
        return meal;
    }

    public async Task<Meal?> GetMealAsync(Guid id)
    {
        var mealRepository = _unitOfWork.GetRepository<Meal>();
        return await mealRepository.GetFirstOrDefaultAsync(
            predicate: m => m.Id == id,
            includes: m => m.Include(m => m.Category));
    }

    public async Task UpdateMealAsync(Guid id, CreateMealDto updateMealDto)
    {
        throw new NotImplementedException();
    }
}
