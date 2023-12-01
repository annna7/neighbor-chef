using neighbor_chef.Models;

namespace neighbor_chef.Services;

public interface IAddressService
{
    Task<Address> CreateAddressAsync(Address address);
    Task<Address> GetAddressAsync(int id);
    Task<Address> UpdateAddressAsync(Address address);
    Task DeleteAddressAsync(int id);
}