namespace neighbor_chef.Models.DTOs.Authentication;

public class PersonRegisterDto
{
    public string Type { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? PictureUrl { get; set; }
    public CreateAddressDto Address { get; set; } = null!;
}