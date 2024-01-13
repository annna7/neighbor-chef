using FirebaseAdmin.Messaging;
using neighbor_chef.Models;
using neighbor_chef.Specifications.Notifications;
using neighbor_chef.UnitOfWork;

namespace neighbor_chef.Services.Notifications;

public class FirebaseNotificationService : IFirebaseNotificationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPersonService _personService;
    
    public FirebaseNotificationService(IUnitOfWork unitOfWork, IPersonService personService)
    {
        _unitOfWork = unitOfWork;
        _personService = personService;
    }
    
    public async Task AddTokenToPerson(Guid personId, string token)
    {
        var firebaseTokenRepository = _unitOfWork.GetRepository<FirebaseToken>();
        var firebaseToken =
            await firebaseTokenRepository.FindFirstOrDefaultWithSpecificationPatternAsync(
                new FindTokenWithPeopleSpecification(token));

        if (firebaseToken == null)
        {
            firebaseToken = new FirebaseToken
            {
                Id = new Guid(),
                Token = token,
                People = new List<Person>()
            };
            await firebaseTokenRepository.AddAsync(firebaseToken);
        }

        if (firebaseToken?.People == null)
        {
            firebaseToken.People = new List<Person>();
        }
        
        var person = firebaseToken.People.FirstOrDefault(x => x.Id == personId);
        if (person == null)
        {
            person = await _personService.GetPersonAsync(personId);
            firebaseToken.People.Add(person);
        }

        await _unitOfWork.CompleteAsync();
    }
    
    public async Task SendNotificationToPerson(Guid personId, string title, string body)
    {
        var firebaseTokenRepository = _unitOfWork.GetRepository<FirebaseToken>();
        var firebaseTokens =
            await firebaseTokenRepository.FindWithSpecificationPatternAsync(
                new FindTokensForPersonSpecification(personId));

        if (firebaseTokens == null || !firebaseTokens.Any())
        {
            return;
        }

        var tokens = firebaseTokens.Select(x => x.Token).ToList();
        var message = new MulticastMessage()
        {
            Tokens = tokens,
            Data = new Dictionary<string, string>()
            {
                {"userId", personId.ToString()},
                {"title", title},
                {"body", body}
            }
        };
        
        var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
        Console.WriteLine($"{response.SuccessCount} messages were sent successfully");
    }
}