namespace neighbor_chef.Models.DTOs.Authentication;

public class ChefRegisterDto : PersonRegisterDto
{
    public string Description { get; set; } = null!;
    public int MaxOrdersPerDay { get; set; }
    public int AdvanceNoticeDays { get; set; }
}