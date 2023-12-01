namespace neighbor_chef.Models.DTOs;

public class PersonRegisterDto
{
    public string Type { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? PictureUrl { get; set; }
    public AddressDto? Address { get; set; } = null!;
}