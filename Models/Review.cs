using System.ComponentModel.DataAnnotations;
using neighbor_chef.Models.Base;

namespace neighbor_chef.Models;

public class Review : BaseEntity
{
    public Guid ChefId { get; set; }
    public virtual Chef Chef { get; set; } = null!;
    
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;
    
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
    public int Rating { get; set; }
    
    public string? Comment { get; set; }
}