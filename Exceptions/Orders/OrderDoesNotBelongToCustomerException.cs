namespace neighbor_chef.Exceptions.Orders;

public class OrderDoesNotBelongToCustomerException : Exception
{
    public OrderDoesNotBelongToCustomerException(Guid orderId, Guid customerId, Guid requestId) : base(
        $"Order with id {orderId} does not belong to customer with id {customerId}, so it cannot be updated by customer with id {requestId}")
    {
    }
}