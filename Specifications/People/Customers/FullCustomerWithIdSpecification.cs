using neighbor_chef.Models;

namespace neighbor_chef.Specifications.People.Customers;

public class FullCustomerWithIdSpecification : BaseSpecification<Customer>
{
    public FullCustomerWithIdSpecification(Guid id)
        : base(customer => customer.Id == id)
    {
        AddInclude(customer => customer.Address);
        AddInclude(customer => customer.ApplicationUser);
        AddInclude(customer => customer.ReviewsLeft);
        AddInclude("OrdersPlaced.OrderMeals.Meal");
    }
}