using neighbor_chef.Models.Base;

namespace neighbor_chef.Models;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();
}