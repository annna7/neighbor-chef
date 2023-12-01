using Microsoft.AspNetCore.Identity;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;

namespace neighbor_chef.Services;

public interface IPersonService
{
    Task<Person> CreatePersonAsync(PersonRegisterDto personRegisterDto);
    Task<Person> GetPersonAsync(string email);
    Task<Person> GetPersonAsync(Guid id);
    Task<Person> UpdatePersonAsync(Person person);
    Task DeletePersonAsync(Person person);
}