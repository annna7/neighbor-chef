using Microsoft.AspNetCore.Identity;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs.Authentication;

namespace neighbor_chef.Controllers;

public interface IAccountService
{
    public Task<IdentityResult> RegisterUserAsync(ApplicationUser user);
    public Task<SignInResult> LoginUserAsync(UserLoginDto userLoginDto);
    public Task LogoutUserAsync();
}