namespace neighbor_chef.Exceptions.Orders;

public class InvalidOrderDateException : Exception
{
    public InvalidOrderDateException(DateTime date, Guid chefId, Guid customerId) : base($"The date {date} is not available for chef {chefId} and customer {customerId}")
    {
    }
}
