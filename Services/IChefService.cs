using neighbor_chef.Models;
using neighbor_chef.Models.DTOs.Authentication;

namespace neighbor_chef.Services;

public interface IChefService : IPersonService
{
    Task<List<Chef>> GetChefsSortedAsync();
}