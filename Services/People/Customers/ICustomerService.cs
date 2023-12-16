using neighbor_chef.Models;

namespace neighbor_chef.Services;

public interface ICustomerService : IPersonService
{
    Task<Customer?> GetCustomerAsync(Guid id, bool asNoTracking = false);
}