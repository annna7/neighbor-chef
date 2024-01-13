using neighbor_chef.Models;
using neighbor_chef.Models.Base;

namespace neighbor_chef.Specifications.Notifications;

public class FindTokensForPersonSpecification : BaseSpecification<FirebaseToken>
{
    public FindTokensForPersonSpecification(Guid personId) : base(x => x.People.Any(p => p.Id == personId))
    {
        AddInclude(x => x.People);
    }
}