using Microsoft.AspNetCore.Identity;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs.Authentication;

namespace neighbor_chef.Services;

public interface IAccountService
{
    public Task<IdentityResult> RegisterUserAsync(ApplicationUser user);
    public Task<SignInResult> LoginUserAsync(UserLoginDto userLoginDto);
    public Task LogoutUserAsync();
    public string GetEmailFromToken(string token);
    public Task AssignRoleAsync(ApplicationUser user, string role);
}