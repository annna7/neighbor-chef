using neighbor_chef.Models;

namespace neighbor_chef.Specifications.People;

public class FullPersonWithEmailSpecification : BaseSpecification<Person>
{
    public FullPersonWithEmailSpecification(string email)
        : base(person => person.ApplicationUser.Email == email)
    {
        AddInclude(person => person.Address);
        AddInclude(person => person.ApplicationUser);
    }
}