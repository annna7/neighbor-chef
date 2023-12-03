using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;
using neighbor_chef.Models.DTOs.Authentication;

namespace neighbor_chef.Services;

public interface IPersonService
{
    Task<Person> CreatePersonAsync(PersonRegisterDto personRegisterDto);
    Task<Person?> GetPersonAsync(string email);
    Task<Person?> GetPersonAsync(Guid id);
    Task<Person> UpdatePersonAsync(Person person);
    Task<Person> UpdatePersonAsync(Guid id, UpdatePersonDto updatePersonDto);
    Task DeletePersonAsync(Person person);
}