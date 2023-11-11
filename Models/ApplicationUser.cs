using Microsoft.AspNetCore.Identity;
using Neighbor_Chef.Models.Base;

namespace Neighbor_Chef.Models;

public class ApplicationUser : IdentityUser<Guid>, IBaseEntity
{
    // using built-in Id property from IdentityUser and inheriting from IBaseEntity
    public DateTime? DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
    
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? ProfilePicture { get; set; }
    
    // using built-in PhoneNumber and Email properties from IdentityUser
    
    public Guid AddressId { get; set; }
    public Address Address { get; set; } = null!;
}