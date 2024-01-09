namespace neighbor_chef.Models.DTOs.Orders;

public class CreateOrderDto
{
    public List<ItemMealDto> MealWithQuantities { get; set; }
    public DateDto DeliveryDate { get; set; }
    public TimeDto DeliveryTime { get; set; }
    public string Observations { get; set; }
}