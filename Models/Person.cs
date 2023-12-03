using neighbor_chef.Models.Base;

namespace neighbor_chef.Models;

public class Person : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string ApplicationUserId { get; set; } = null!;
    public virtual ApplicationUser ApplicationUser { get; set; } = null!;
    
    public Guid AddressId { get; set; }
    public virtual Address? Address { get; set; }
    
    public string? ProfilePictureUrl { get; set; }
}