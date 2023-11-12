using neighbor_chef.Models;

namespace neighbor_chef.TokenService;

public interface ITokenService
{
    string CreateToken(ApplicationUser user, IList<string> roles);
}