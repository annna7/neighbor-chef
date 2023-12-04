using neighbor_chef.Models;

namespace neighbor_chef.Specifications;

public class ChefNameAscending : BaseSpecification<Chef>
{
    public ChefNameAscending(char firstLetter)
        : base(chef => chef.LastName.StartsWith(firstLetter.ToString()))
    {
        AddOrderBy(chef => chef.LastName);
        AddInclude(chef => chef.Meals);
    }
}