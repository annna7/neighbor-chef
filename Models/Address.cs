using Neighbor_Chef.Models.Base;

namespace Neighbor_Chef.Models;

public class Address : BaseEntity
{
    public Guid Id { get; set; }
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string County { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string StreetNumber { get; set; } = null!;
    public string? Floor { get; set; }
    public string? Apartment { get; set; }
}