using neighbor_chef.Models.Base;

namespace neighbor_chef.Models;

public class OrderMeal : BaseEntity
{
    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; } = null!;

    public Guid MealId { get; set; }
    public virtual Meal Meal { get; set; } = null!;
}
