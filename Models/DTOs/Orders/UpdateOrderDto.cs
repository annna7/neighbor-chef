namespace neighbor_chef.Models.DTOs.Orders;

public class UpdateOrderDto
{
    public string? Observations { get; set; }
    public DateDto? DeliveryDate { get; set; }
    public TimeDto? DeliveryTime { get; set; }
    public List<Guid>? MealIds { get; set; }
}