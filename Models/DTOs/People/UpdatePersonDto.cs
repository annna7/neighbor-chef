using neighbor_chef.Models.DTOs.People;

namespace neighbor_chef.Models.DTOs;

public class UpdatePersonDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public UpdateApplicationUserDto? ApplicationUser { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public UpdateAddressDto? Address { get; set; }
    
    public string? Description { get; set; }
    public int? AdvanceNoticeDays { get; set; }
    public int? MaxOrdersPerDay { get; set; }
}


