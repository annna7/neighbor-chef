using Microsoft.AspNetCore.Identity;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;
using neighbor_chef.Models.DTOs.Authentication;
using neighbor_chef.Specifications;
using neighbor_chef.Specifications.People;
using neighbor_chef.UnitOfWork;

namespace neighbor_chef.Services;

public class PersonService : IPersonService
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IAccountService _accountService;
    
    public PersonService(IUnitOfWork unitOfWork, IAccountService accountService)
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
            EmailConfirmed = true,
            PhoneNumber = personDto.PhoneNumber,
            PasswordHash = personDto.Password,
        };
        
        // await _userManager.CreateAsync(user, personDto.Password);
        
        var address = new Address
        {
            StreetNumber = personDto.Address.StreetNumber,
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

    public Task<Person?> GetPersonAsync(string email, bool asNoTracking = false)
    {
        var personRepository = _unitOfWork.GetRepository<Person>();
        var fullPersonByEmailSpecification = new FullPersonWithEmailSpecification(email);
        return personRepository.FindFirstOrDefaultWithSpecificationPatternAsync(fullPersonByEmailSpecification, asNoTracking);
    }

    public async Task<Person?> GetPersonAsync(Guid id)
    {
        var personRepository = _unitOfWork.GetRepository<Person>();
        var partialPerson = await personRepository.GetByIdAsync(id);
        if (partialPerson == null) return null;
        var fullPersonByIdSpecification = new FullPersonWithIdSpecification(id);
        return await personRepository.FindFirstOrDefaultWithSpecificationPatternAsync(fullPersonByIdSpecification);
    }

    public async Task<Person> UpdatePersonAsync(Person person)
    {
        var personRepository = _unitOfWork.GetRepository<Person>();
        await personRepository.UpdateAsync(person);
        await _unitOfWork.CompleteAsync();
        return person;
    }
    
    public async Task<Person> UpdatePersonAsync(Guid id, UpdatePersonDto updateDto)
    {
        var personRepository = _unitOfWork.GetRepository<Person>();
        var fullPersonByIdSpecification = new FullPersonWithIdSpecification(id);
        var person = await personRepository.FindFirstOrDefaultWithSpecificationPatternAsync(fullPersonByIdSpecification);

        if (person == null) throw new KeyNotFoundException("Person not found.");

        if (!string.IsNullOrEmpty(updateDto.FirstName))
            person.FirstName = updateDto.FirstName;
        if (!string.IsNullOrEmpty(updateDto.LastName))
            person.LastName = updateDto.LastName;
        if (!string.IsNullOrEmpty(updateDto.ApplicationUser.PhoneNumber))
            person.ApplicationUser.PhoneNumber = updateDto.ApplicationUser.PhoneNumber;
        if (!string.IsNullOrEmpty(updateDto.ProfilePictureUrl))
            person.ProfilePictureUrl = updateDto.ProfilePictureUrl;

        if (updateDto.Address != null)
        {
            var address = person.Address;
            address.Street = updateDto.Address.Street ?? address.Street;
            address.City = updateDto.Address.City ?? address.City;
            address.County = updateDto.Address.County ?? address.County;
            address.Country = updateDto.Address.Country ?? address.Country;
            address.ZipCode = updateDto.Address.ZipCode ?? address.ZipCode;
            address.StreetNumber = updateDto.Address.StreetNumber ?? address.StreetNumber;
            address.Apartment = updateDto.Address.ApartmentNumber ?? address.Apartment;
            
            await _unitOfWork.GetRepository<Address>().UpdateAsync(address);
        }

        if (!string.IsNullOrEmpty(updateDto.Description) || updateDto.MaxOrdersPerDay.HasValue || updateDto.AdvanceNoticeDays.HasValue)
        {
            var chefRepository = _unitOfWork.GetRepository<Chef>();
            var chef = await chefRepository.FindFirstOrDefaultWithSpecificationPatternAsync(new FullChefWithIdSpecification(id));
            if (chef == null) throw new KeyNotFoundException("Chef not found.");
            chef.Description = updateDto.Description ?? chef.Description;
            chef.MaxOrdersPerDay = updateDto.MaxOrdersPerDay ?? chef.MaxOrdersPerDay;
            chef.AdvanceNoticeDays = updateDto.AdvanceNoticeDays ?? chef.AdvanceNoticeDays;
            await chefRepository.UpdateAsync(chef);
        }
        
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