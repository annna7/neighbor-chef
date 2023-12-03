using Microsoft.AspNetCore.Mvc;
using neighbor_chef.Models;
using neighbor_chef.Services;

namespace neighbor_chef.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    
    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }
    
    [HttpGet("getByEmail/{email}")]
    public async Task<IActionResult> GetPersonByEmail(string email)
    {
        var person = await _personService.GetPersonAsync(email);
        if (person == null) return NotFound("Person with email " + email + " not found.");
        return Ok(person);
    }

    [HttpGet("getById/{id:guid}")]
    public async Task<IActionResult> GetPersonById(Guid id)
    {
        var person = await _personService.GetPersonAsync(id);
        if (person == null) return NotFound("Person with id " + id + " not found.");
        return Ok(person);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdatePerson(Person person)
    {
        var updatedPerson = await _personService.UpdatePersonAsync(person);
        return Ok(updatedPerson);
    }

    [HttpDelete("delete/{id:guid}")]
    public async Task<IActionResult> DeletePerson(Guid id)
    {
        var person = await _personService.GetPersonAsync(id);
        if (person == null) return NotFound("Person with id " + id + " not found.");
        await _personService.DeletePersonAsync(person);
        return Ok();
    }
}