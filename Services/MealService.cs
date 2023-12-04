using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;
using neighbor_chef.UnitOfWork;
using Newtonsoft.Json;

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
            IngredientsJson= JsonConvert.SerializeObject(createMealDto.Ingredients)
        };

        await _unitOfWork.GetRepository<Meal>().AddAsync(meal);
        await _unitOfWork.CompleteAsync();

        return meal;
    }

    public async Task<Meal?> GetMealAsync(Guid id)
    {
        var mealRepository = _unitOfWork.GetRepository<Meal>();
        return await mealRepository.GetFirstOrDefaultAsync(
            predicate: m => m.Id == id);
    }

    public async Task<Meal> UpdateMealAsync(Guid id, UpdateMealDto updateMealDto)
    {
        var mealRepository = _unitOfWork.GetRepository<Meal>();
        var meal = await mealRepository.GetByIdAsync(id);
        if (meal == null)
        {
            throw new KeyNotFoundException("Meal not found.");
        }

        meal.Name = updateMealDto.Name ?? meal.Name;
        meal.Description = updateMealDto.Description ?? meal.Description;
        meal.PictureUrl = updateMealDto.PictureUrl ?? meal.PictureUrl;
        meal.Price = updateMealDto.Price ?? meal.Price;

        await mealRepository.UpdateAsync(meal);
        await _unitOfWork.CompleteAsync();

        return meal;
    }

    public async Task<Meal> AddIngredientAsync(Guid id, AddIngredientDto addIngredientDto)
    {
        var mealRepository = _unitOfWork.GetRepository<Meal>();
        var meal = await mealRepository.GetByIdAsync(id);
        if (meal == null)
        {
            throw new KeyNotFoundException("Meal not found.");
        }

        var ingredients = meal.Ingredients ?? new List<string>();
        ingredients.Add(addIngredientDto.Name);
        
        meal.IngredientsJson = JsonConvert.SerializeObject(ingredients);

        await mealRepository.UpdateAsync(meal);
        await _unitOfWork.CompleteAsync();

        return meal;
    }

    public async Task<Meal> RemoveIngredientAsync(Guid id, RemoveIngredientDto removeIngredientDto)
    {
        var mealRepository = _unitOfWork.GetRepository<Meal>();
        var meal = await mealRepository.GetByIdAsync(id);
        if (meal == null)
        {
            throw new KeyNotFoundException("Meal not found.");
        }
        
        var ingredients = meal.Ingredients ?? new List<string>();
        if (ingredients.Find(i => i == removeIngredientDto.Name) == null)
        {
            throw new KeyNotFoundException("Ingredient " + removeIngredientDto.Name + " not found in " + meal.Name);
        }
        
        meal.IngredientsJson = JsonConvert.SerializeObject(ingredients);

        await mealRepository.UpdateAsync(meal);
        await _unitOfWork.CompleteAsync();

        return meal;
    }

    public async Task<Meal> DeleteMealAsync(Guid id)
    {
        var mealRepository = _unitOfWork.GetRepository<Meal>();
        var meal = await mealRepository.GetByIdAsync(id);
        if (meal == null)
        {
            throw new KeyNotFoundException("Meal not found.");
        }

        await mealRepository.DeleteAsync(meal);
        await _unitOfWork.CompleteAsync();

        return meal;
    }
}
