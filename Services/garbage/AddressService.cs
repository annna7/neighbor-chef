using neighbor_chef.Models;
using neighbor_chef.UnitOfWork;

namespace neighbor_chef.Services;

public class AddressService : GenericService, IAddressService
{
    public AddressService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    public async  Task<Address> CreateAddressAsync(Address address)
    {
        var addressRepo = UnitOfWork.GetRepository<Address>();
        await addressRepo.AddAsync(address);
        await UnitOfWork.CompleteAsync();
        return address;
    }

    public Task<Address> GetAddressAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Address> UpdateAddressAsync(Address address)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAddressAsync(int id)
    {
        throw new NotImplementedException();
    }
}