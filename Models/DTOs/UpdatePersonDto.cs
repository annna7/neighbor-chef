namespace neighbor_chef.Models.DTOs;

public class UpdatePersonDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public UpdateAddressDto? Address { get; set; }
}


