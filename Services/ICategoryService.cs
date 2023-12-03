using neighbor_chef.Models;

namespace neighbor_chef.Services;

public interface ICategoryService
{
    Task<Category> CreateCategoryAsync(string name);
    Task<Category?> GetCategoryAsync(Guid id);
    Task<Category?> GetCategoryAsync(string name);
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(Guid id);
}
