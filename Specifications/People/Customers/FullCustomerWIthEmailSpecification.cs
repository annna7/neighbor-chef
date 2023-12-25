using neighbor_chef.Models;

namespace neighbor_chef.Specifications.People.Customers;

public class FullCustomerWithEmailSpecification : BaseSpecification<Customer>
{
    public FullCustomerWithEmailSpecification(string email)
        : base(customer => customer.ApplicationUser.Email == email)
    {
        AddInclude(customer => customer.Address);
        AddInclude(customer => customer.ApplicationUser);
        AddInclude(customer => customer.ReviewsLeft);
        AddInclude(customer => customer.OrdersPlaced);
    }
}