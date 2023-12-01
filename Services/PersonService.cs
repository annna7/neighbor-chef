using Microsoft.AspNetCore.Identity;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;
using neighbor_chef.UnitOfWork;

namespace neighbor_chef.Services;

public class PersonService : IPersonService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AccountService _accountService;

    protected PersonService(IUnitOfWork unitOfWork, AccountService accountService)
    {
        _unitOfWork = unitOfWork;
        _accountService = accountService;
    } 
    
    public virtual async Task<Person> CreatePersonAsync(PersonRegisterDto personDto)
    {
        var user = new ApplicationUser
        {
            Email = personDto.Email,
            UserName = personDto.Email,
            PasswordHash = personDto.Password,
            EmailConfirmed = true
        };
        
        await _accountService.RegisterUserAsync(user);

        if (personDto.Address != null)
        {
            var address = new Address
            {
                StreetNumber = personDto.Address?.StreetNumber,
                County = personDto.Address.County,
                Street = personDto.Address.Street,
                City = personDto.Address.City,
                Country = personDto.Address.Country,
                ZipCode = personDto.Address.ZipCode,
                Apartment = personDto.Address?.ApartmentNumber
            };
        
            await _unitOfWork.GetRepository<Address>().AddAsync(address);
            await _unitOfWork.CompleteAsync();
        }
        
        var person = new Person
        {
            FirstName = personDto.FirstName,
            LastName = personDto.LastName,
            ApplicationUser = user
        };
        
        await _unitOfWork.GetRepository<Person>().AddAsync(person);
        await _unitOfWork.CompleteAsync();
        
        return person;
    }

    public Task<Person> GetPersonAsync(string email)
    {
        var personRepository = _unitOfWork.GetRepository<Person>();
        return personRepository.GetFirstOrDefaultAsync(predicate: p => p.ApplicationUser.Email == email);
    }

    public Task<Person> GetPersonAsync(Guid id)
    {
        var personRepository = _unitOfWork.GetRepository<Person>();
        return personRepository.GetByIdAsync(id);
    }

    public Task<Person> UpdatePersonAsync(Person person)
    {
        var personRepository = _unitOfWork.GetRepository<Person>();
        personRepository.UpdateAsync(person);
        return Task.FromResult(person);
    }

    public Task DeletePersonAsync(Person person)
    {
        var personRepository = _unitOfWork.GetRepository<Person>();
        personRepository.DeleteAsync(person);
        return Task.CompletedTask;
    }
}