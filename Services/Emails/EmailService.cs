using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using Task = System.Threading.Tasks.Task;

namespace neighbor_chef.Services.Emails;

public class EmailService : IEmailService
{
    private readonly TransactionalEmailsApi _api;
    private readonly string _senderName = "Neighbor Chef";
    private readonly string _senderEmail = "neighborchef7@gmail.com";
    
    public EmailService()
    {
        Configuration.Default.AddApiKey("api-key",
            "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");
        Console.WriteLine("MY API KEY", System.Environment.GetEnvironmentVariable("SENDINBLUE_API_KEY"));
        _api = new TransactionalEmailsApi();
    }
    
    public async Task SendEmailAsync(string to, string imageLink, string message)
    {
        var dynamicTemplateData = new
        {
            link = imageLink,
            text = message
        };
            

        var recipient = new SendSmtpEmailTo(to, to);
        
        var sender = new SendSmtpEmailSender
        {
            Name = _senderName,
            Email = _senderEmail
        };

        var email = new SendSmtpEmail(sender)
        {
            To = new List<SendSmtpEmailTo> { recipient },
            TemplateId = 1,
            Params = dynamicTemplateData
        };
        
        try
        {
            await _api.SendTransacEmailAsync(email);
        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling TransactionalEmailsApi.SendTransacEmail: " + e.Message);
        }
    }
}