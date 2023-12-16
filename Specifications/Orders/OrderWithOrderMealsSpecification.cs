using neighbor_chef.Models;

namespace neighbor_chef.Specifications.Orders;

public class OrderWithOrderMealsSpecification : BaseSpecification<Order>
{
    public OrderWithOrderMealsSpecification(Guid orderId)
        : base(order => order.Id == orderId)
    {
        AddInclude(order => order.OrderMeals);
        // AddInclude($"{nameof(Order.OrderMeals)}.{nameof(OrderMeal.Meal)}");
    }
}