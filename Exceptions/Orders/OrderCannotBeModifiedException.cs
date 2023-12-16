using neighbor_chef.Models;

namespace neighbor_chef.Exceptions.Orders;

public class OrderCannotBeModifiedException : Exception
{
    public OrderCannotBeModifiedException(Guid orderId, OrderStatus orderStatus) : 
        base($"Order with id {orderId} cannot be modified because it is in status {orderStatus}.") { }
}