using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;
using neighbor_chef.Models.DTOs.Authentication;

namespace neighbor_chef.Services;

public interface IChefService : IPersonService
{
    Task<Chef?> GetChefAsync(Guid id, bool asNoTracking = false);
    Task<string> AddAvailableDateAsync(Guid chefId, DateDto date);
    Task<List<DateTime>> GetAvailableDatesAsync(Guid chefId);
    Task RemoveAvailableDateAsync(Guid chefId, DateDto date);
    Task<bool> IsDateAvailable(Chef chef, DateTime date);
    Task<List<Chef>> GetAllChefsAsync(bool asNoTracking = false);
}