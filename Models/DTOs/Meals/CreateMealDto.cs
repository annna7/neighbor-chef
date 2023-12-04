namespace neighbor_chef.Models.DTOs;

public class CreateMealDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string? PictureUrl { get; set; }
    public decimal Price { get; set; }
    public List<string>? Ingredients { get; set; }
}