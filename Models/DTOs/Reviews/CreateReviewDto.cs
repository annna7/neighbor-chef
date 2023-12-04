namespace neighbor_chef.Models.DTOs.Reviews;

public class CreateReviewDto
{
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public Guid ChefId { get; set; }
}