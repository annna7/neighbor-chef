using neighbor_chef.Models;

namespace neighbor_chef.Services;

public interface ICustomerService : IPersonService
{
    Task<Customer?> GetCustomerAsync(string email, bool asNoTracking = false);
    Task<Customer?> GetCustomerAsync(Guid id, bool asNoTracking = false);
    Task<List<Customer>> GetAllCustomersAsync(bool asNoTracking = false);
}