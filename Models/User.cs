using neighbor_chef.Models.Base;

namespace neighbor_chef.Models;

public class User : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    
    public string ApplicationUserId { get; set; } = null!;
    public virtual ApplicationUser ApplicationUser { get; set; } = null!;
    
    public string AddressId { get; set; } = null!;
    public virtual Address Address { get; set; } = null!;
    
    public string? ProfilePictureUrl { get; set; }
}