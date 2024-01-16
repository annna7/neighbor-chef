namespace neighbor_chef.Services.Emails;

public interface IEmailService
{
    Task SendEmailAsync(string to, string imageLink, string message);
}