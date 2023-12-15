using System.ComponentModel.DataAnnotations;

namespace neighbor_chef.Models.DTOs;

public class DateDto
{
    [Range(1, 31, ErrorMessage = "Day must be between 1 and 31")]
    public int Day { get; set; }
    [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]
    public int Month { get; set; }
    [Range(2023, 2024, ErrorMessage = "Year must be between this year and next year")]
    public int Year { get; set; }
}