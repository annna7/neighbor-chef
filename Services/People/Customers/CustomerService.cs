using AutoMapper;
using Microsoft.AspNetCore.Identity;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs.Authentication;
using neighbor_chef.Specifications.People.Customers;
using neighbor_chef.UnitOfWork;
using Newtonsoft.Json;

namespace neighbor_chef.Services;

public class CustomerService : PersonService, ICustomerService
{
    public CustomerService(IUnitOfWork unitOfWork, IAccountService accountService) : base(unitOfWork, accountService)
    {
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
    
    public async Task<Customer?> GetCustomerAsync(Guid id, bool asNoTracking = false)
    {
        var customer = await _unitOfWork.GetRepository<Customer>().FindFirstOrDefaultWithSpecificationPatternAsync(new FullCustomerWithIdSpecification(id), asNoTracking);
        return customer;
    }
    
    public async Task<List<Customer>> GetAllCustomersAsync(bool asNoTracking = false)
    {
        var customers = await _unitOfWork.GetRepository<Customer>().GetAllAsync(asNoTracking);
        return customers;
    }
    
    public async Task<Customer?> GetCustomerAsync(string email, bool asNoTracking = false)
    {
        var customer = await _unitOfWork.GetRepository<Customer>().FindFirstOrDefaultWithSpecificationPatternAsync(new FullCustomerWithEmailSpecification(email), asNoTracking);
        return customer;
    }
}