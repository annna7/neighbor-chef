using Microsoft.AspNetCore.Identity;

namespace neighbor_chef.Models.DTOs.People;

public class UpdateApplicationUserDto
{
    public string? PhoneNumber { get; set; }
}