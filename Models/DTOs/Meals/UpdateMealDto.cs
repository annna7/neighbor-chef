namespace neighbor_chef.Models.DTOs;

public class UpdateMealDto
{
    public string? Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public string? PictureUrl { get; set; }
    public decimal? Price { get; set; }
    public string? CategoryName { get; set; } = null!;
    public List<string>? Ingredients { get; set; }
}