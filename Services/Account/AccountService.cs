using Microsoft.AspNetCore.Identity;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs.Authentication;
using System.IdentityModel.Tokens.Jwt;


namespace neighbor_chef.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        

        public string GetEmailFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var email = jsonToken?.Claims.First(claim => claim.Type == "email").Value;

            if (email == null)
            {
                throw new Exception("No email claim found.");
            }
            return email;
        }


        public async Task<IdentityResult> RegisterUserAsync(ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user, user.PasswordHash);

            return result;
        }

        public async Task<SignInResult> LoginUserAsync(UserLoginDto userLoginDto)
        {
            return await _signInManager.PasswordSignInAsync(userLoginDto.Email, userLoginDto.Password, isPersistent: false, lockoutOnFailure: false);
        }

        public async Task LogoutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }
        
        public async Task AssignRoleAsync(ApplicationUser user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }
    }
}