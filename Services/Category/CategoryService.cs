using neighbor_chef.Models;
using neighbor_chef.Services;
using neighbor_chef.Specifications.Categories;
using neighbor_chef.UnitOfWork;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Category> CreateCategoryAsync(string name)
    {
        var category = new Category { Name = name };
        await _unitOfWork.GetRepository<Category>().AddAsync(category);
        await _unitOfWork.CompleteAsync();
        return category;
    }

    public async Task<Category?> GetCategoryAsync(Guid id)
    {
        return await _unitOfWork.GetRepository<Category>().GetByIdNoTrackingAsync(id);
    }
    
    public async Task<Category?> GetCategoryAsync(string name)
    {
        var categorySpecification = new CategoryNameSpecification(name);
        return await _unitOfWork.GetRepository<Category>().FindFirstOrDefaultWithSpecificationPatternAsync(categorySpecification);
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _unitOfWork.GetRepository<Category>().GetAllAsync();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        await _unitOfWork.GetRepository<Category>().UpdateAsync(category);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        var category = await GetCategoryAsync(id);
        if (category != null)
        {
            await _unitOfWork.GetRepository<Category>().DeleteAsync(category);
            await _unitOfWork.CompleteAsync();
        }
    }
}