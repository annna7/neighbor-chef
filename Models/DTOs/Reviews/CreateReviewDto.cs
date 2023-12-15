using System.ComponentModel.DataAnnotations;

namespace neighbor_chef.Models.DTOs.Reviews;

public class CreateReviewDto
{
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public Guid ChefId { get; set; }
}