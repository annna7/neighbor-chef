namespace neighbor_chef.Models.DTOs;

public class AddressDto
{
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string County { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string? StreetNumber { get; set; }
    public string? ApartmentNumber { get; set; }
}