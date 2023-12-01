using AutoMapper;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;
using neighbor_chef.UnitOfWork;

namespace neighbor_chef.Services;

public class CustomerService : PersonService, ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AccountService _accountService;
    private readonly IMapper _mapper;
    
    public CustomerService(IUnitOfWork unitOfWork, AccountService accountService, IMapper mapper) : base(unitOfWork, accountService)
    {
        _mapper = mapper;
    }
    
    public override async Task<Person> CreatePersonAsync(PersonRegisterDto customerDto)
    {
        Console.WriteLine(customerDto);
        customerDto = (CustomerRegisterDto) customerDto;
        var customer = await base.CreatePersonAsync(customerDto);
        await _accountService.RegisterUserAsync(customer.ApplicationUser);
        await _accountService.AssignRoleAsync(customer.ApplicationUser, "Customer");

        var customerObject = _mapper.Map<Customer>(customerDto);
        
        // map existing person to chef
        _mapper.Map(customer, customerObject);

        await _unitOfWork.GetRepository<Person>().AddAsync(customerObject);
        await _unitOfWork.CompleteAsync();
        return customer;
    }
}