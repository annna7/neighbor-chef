using AutoMapper;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;
using neighbor_chef.UnitOfWork;
using Newtonsoft.Json;

namespace neighbor_chef.Services;

public class CustomerService : PersonService, ICustomerService
{
    protected readonly IMapper _mapper;
    
    public CustomerService(IUnitOfWork unitOfWork, AccountService accountService, IMapper mapper) : base(unitOfWork, accountService)
    {
        _mapper = mapper;
    }
    
    public override async Task<Person> CreatePersonAsync(PersonRegisterDto customerDto)
    {
        if (customerDto is not CustomerRegisterDto myCustomerDto)
        {
            throw new Exception("Invalid customer DTO.");
        }
        
        customerDto = myCustomerDto;
        
        var customer  = await base.CreatePersonAsync(customerDto);
        
        var jsonString = JsonConvert.SerializeObject(customer.ApplicationUser, Formatting.Indented);
        Console.WriteLine(jsonString);
        
        await _accountService.RegisterUserAsync(customer.ApplicationUser);
        await _accountService.AssignRoleAsync(customer.ApplicationUser, "Customer");

        var customerObject = new Customer
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            ApplicationUser = customer.ApplicationUser,
            ApplicationUserId = customer.ApplicationUserId,
            Address = customer.Address,
            AddressId = customer.AddressId,
            Id = customer.Id,
            DateCreated = customer.DateCreated,
            DateModified = customer.DateModified,
            ProfilePictureUrl = customer.ProfilePictureUrl,
        };

        await _unitOfWork.GetRepository<Customer>().AddAsync(customerObject);
        await _unitOfWork.CompleteAsync();
        
        return customerObject;
    }
}