using neighbor_chef.Models;

namespace neighbor_chef.Specifications.Orders;

public class MealWithCategoryNameSpecification : BaseSpecification<Meal>
{
    public MealWithCategoryNameSpecification() : base(null)
    {
        AddInclude(meal => meal.Category);
    }
}