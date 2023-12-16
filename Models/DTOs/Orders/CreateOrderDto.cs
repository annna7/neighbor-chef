namespace neighbor_chef.Models.DTOs.Orders;

public class CreateOrderDto
{
    public List<Guid> MealIds { get; set; }
    public DateDto DeliveryDate { get; set; }
    public TimeDto DeliveryTime { get; set; }
    public string Observations { get; set; }
}