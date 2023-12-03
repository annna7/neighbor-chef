using Microsoft.AspNetCore.Mvc;
using neighbor_chef.Models.DTOs;

namespace neighbor_chef.Controllers;

public interface ICategoryController
{
    Task<IActionResult> CreateCategory(CreateCategoryDto name);
    Task<IActionResult> GetCategoryByName(string name);
    Task<IActionResult> GetCategoryById(Guid id);
    Task<IActionResult> GetAllCategories();
    Task<IActionResult> DeleteCategory(Guid id);
}