namespace neighbor_chef.Models.DTOs.Authentication;

public class UserLoginDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}