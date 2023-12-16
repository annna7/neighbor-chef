using neighbor_chef.Models.Base;

namespace neighbor_chef.Models;

public class Order : BaseEntity
{
    public Guid ChefId { get; set; }
    public virtual Chef Chef { get; set; } = null!;
    
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;
    
    public string Observations { get; set; } = null!;
    public DateTime DeliveryDate { get; set; }
    public OrderStatus Status { get; set; }
    
    public virtual ICollection<OrderMeal> OrderMeals { get; set; } = new List<OrderMeal>();    
}