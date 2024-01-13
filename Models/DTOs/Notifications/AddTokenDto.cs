namespace neighbor_chef.Models.DTOs.Notifications;

public class AddTokenDto
{
    public Guid PersonId { get; set; }
    public string Token { get; set; }
}