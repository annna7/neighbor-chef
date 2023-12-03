namespace neighbor_chef.Models.DTOs;

public class CreateAddressDto
{
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string County { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string StreetNumber { get; set; } = null!;
    public string? ApartmentNumber { get; set; }
}