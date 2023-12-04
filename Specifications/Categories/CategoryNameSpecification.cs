using neighbor_chef.Models;

namespace neighbor_chef.Specifications.Categories;

public class CategoryNameSpecification : BaseSpecification<Category>
{
    public CategoryNameSpecification(string name)
        : base(category => category.Name == name)
    {
    }
}