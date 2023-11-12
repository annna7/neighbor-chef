namespace neighbor_chef.Models.DTOs;

public class ChefRegisterDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int MaxOrdersPerDay { get; set; }
    public int AdvanceNoticeDays { get; set; }
}