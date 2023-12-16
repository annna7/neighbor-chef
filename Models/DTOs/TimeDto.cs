using System.ComponentModel.DataAnnotations;

namespace neighbor_chef.Models.DTOs;

public class TimeDto
{
    [Range(0, 23, ErrorMessage = "Hour must be between 0 and 23.")]
    public int Hour { get; set; }
    [Range(0, 59, ErrorMessage = "Minute must be between 0 and 59.")]
    public int Minute { get; set; }
}