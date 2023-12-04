using neighbor_chef.Models;

namespace neighbor_chef.Specifications.People;

public class FullPersonWithIdSpecification : BaseSpecification<Person>
{
    public FullPersonWithIdSpecification(Guid id)
        : base(person => person.Id == id)
    {
        AddInclude(person => person.Address);
        AddInclude(person => person.ApplicationUser);
    }
}