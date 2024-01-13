namespace neighbor_chef.Services.Notifications;

public interface IFirebaseNotificationService
{
    Task AddTokenToPerson(Guid personId, string token);
    Task SendNotificationToPerson(Guid personId, string title, string body);
}