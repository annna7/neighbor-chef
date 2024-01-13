using Microsoft.AspNetCore.Mvc;
using neighbor_chef.Models.DTOs.Notifications;
using neighbor_chef.Services.Notifications;

namespace neighbor_chef.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IFirebaseNotificationService _notificationService;
    private readonly HttpClient _http;
    
    public NotificationController(IFirebaseNotificationService notificationService, HttpClient http)
    {
        _notificationService = notificationService;
        _http = http;
    }
    
    [HttpPost("token")]
    public async Task<IActionResult> AddTokenToPerson([FromBody] AddTokenDto addTokenDto)
    {
        await _notificationService.AddTokenToPerson(addTokenDto.PersonId, addTokenDto.Token);
        return NoContent();
    }
}