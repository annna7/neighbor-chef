using neighbor_chef.Models;

namespace neighbor_chef.Specifications;

public class OrdersForChefAndDate : BaseSpecification<Order>
{
    public OrdersForChefAndDate(Guid chefId)
        : base(order => order.ChefId == chefId)
    {
        AddInclude(order => order.Customer);
    }
    
    public OrdersForChefAndDate(Guid chefId, DateTime date)
        : base(order => order.ChefId == chefId && order.DeliveryDate == date)
    {
        AddInclude(order => order.Customer);
    }
}