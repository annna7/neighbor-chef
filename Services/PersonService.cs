using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;
using neighbor_chef.UnitOfWork;

namespace neighbor_chef.Services;

public class PersonService : IPersonService
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly AccountService _accountService;

    public PersonService(IUnitOfWork unitOfWork, AccountService accountService)
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
            EmailConfirmed = true,
            PhoneNumber = personDto.PhoneNumber,
        };
        
        var address = new Address
        {
            StreetNumber = personDto.Address.StreetNumber ?? "NaN",
            County = personDto.Address.County,
            Street = personDto.Address.Street,
            City = personDto.Address.City,
            Country = personDto.Address.Country,
            ZipCode = personDto.Address.ZipCode,
            Apartment = personDto.Address?.ApartmentNumber
        };
    
        await _unitOfWork.GetRepository<Address>().AddAsync(address);
        await _unitOfWork.CompleteAsync();
        
        var person = new Person
        {
            FirstName = personDto.FirstName,
            LastName = personDto.LastName,
            AddressId = address.Id,
            Address = address,
            ApplicationUser = user,
            ApplicationUserId = user.Id,
        };
        
        return person;
    }

    public Task<Person?> GetPersonAsync(string email)
    {
        var personRepository = _unitOfWork.GetRepository<Person>();
        return personRepository.GetFirstOrDefaultAsync(predicate: p => p.ApplicationUser.Email == email);
    }

    public async Task<Person?> GetPersonAsync(Guid id)
    {
        var personRepository = _unitOfWork.GetRepository<Person>();
        var person = await personRepository.GetFirstOrDefaultAsync(
            p => p.Id == id,
            includes: query => query
                .Include(p => p.Address)
                .Include(p => p.ApplicationUser));
        return person;
    }

    public async Task<Person> UpdatePersonAsync(Person person)
    {
        var personRepository = _unitOfWork.GetRepository<Person>();
        await personRepository.UpdateAsync(person);
        await _unitOfWork.CompleteAsync();
        return person;
    }

    public async Task DeletePersonAsync(Person person)
    {
        var personRepository = _unitOfWork.GetRepository<Person>();
        await personRepository.DeleteAsync(person);
        await _unitOfWork.CompleteAsync();
    }
}