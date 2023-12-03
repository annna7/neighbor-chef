using Microsoft.AspNetCore.Mvc;
using neighbor_chef.Models;

namespace neighbor_chef.Controllers;

public interface IPersonController
{
    Task<IActionResult> GetPersonByEmail(string email);
    Task<IActionResult> GetPersonById(Guid id);
    Task<IActionResult> UpdatePerson(Person person);
    Task<IActionResult> DeletePerson(Guid id);
}