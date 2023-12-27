using neighbor_chef.Models;

namespace neighbor_chef.Specifications;

public class FullChefWithEmailSpecification : BaseSpecification<Chef>
{
    public FullChefWithEmailSpecification(string email)
        : base(chef => chef.ApplicationUser.Email == email)
    {
        AddInclude(chef => chef.Address);
        AddInclude(chef => chef.ApplicationUser);
        AddInclude(chef => chef.ReviewsReceived);
        AddInclude(chef => chef.OrdersReceived);
        AddInclude(chef => chef.Meals);
    }
}