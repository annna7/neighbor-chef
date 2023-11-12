namespace neighbor_chef.Models;

public class Customer : Person
{
    public virtual ICollection<Review> ReviewsLeft { get; set; } = new List<Review>();
    
    public virtual ICollection<Order> OrdersPlaced { get; set; } = new List<Order>();
}