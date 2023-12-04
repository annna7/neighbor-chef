namespace neighbor_chef.Models.DTOs;

public class UpdateAddressDto
{
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? County { get; set; }
    public string? Country { get; set; }
    public string? ZipCode { get; set; }
    public string? StreetNumber { get; set; }
    public string? ApartmentNumber { get; set; }
}