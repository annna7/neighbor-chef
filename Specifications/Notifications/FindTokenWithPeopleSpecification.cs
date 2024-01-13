using neighbor_chef.Models;

namespace neighbor_chef.Specifications.Notifications;

public class FindTokenWithPeopleSpecification : BaseSpecification<FirebaseToken>
{
    public FindTokenWithPeopleSpecification(string token) : base(x => x.Token == token)
    {
        AddInclude(x => x.People);
    }
}