using neighbor_chef.Models;

namespace neighbor_chef.Specifications;

public class FullChefWithIdSpecification : BaseSpecification<Chef>
{
    public FullChefWithIdSpecification(Guid id)
        : base(chef => chef.Id == id)
    {
        AddInclude(chef => chef.Address);
        AddInclude(chef => chef.ApplicationUser);
        AddInclude(chef => chef.ReviewsReceived);
        AddInclude(chef => chef.OrdersReceived);
        AddInclude(chef => chef.Meals);
    }
}