using neighbor_chef.Models;

namespace neighbor_chef.Specifications;

public class FullAllChefsSpecification: BaseSpecification<Chef>
{
    public FullAllChefsSpecification() : base(chef => true)
    {
        AddInclude(chef => chef.ApplicationUser);
        AddInclude(chef => chef.ReviewsReceived);
        AddInclude(chef => chef.OrdersReceived);
        AddInclude(chef => chef.Meals);
    }
}