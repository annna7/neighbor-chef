using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;
using neighbor_chef.Models.DTOs.Authentication;

namespace neighbor_chef.Services;

public interface IChefService : IPersonService
{
    Task<string> AddAvailableDateAsync(Guid chefId, DateDto date);
    Task<List<DateTime>> GetAvailableDatesAsync(Guid chefId);
    Task RemoveAvailableDateAsync(Guid chefId, DateDto date);
}