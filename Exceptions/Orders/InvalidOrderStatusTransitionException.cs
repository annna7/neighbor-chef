using neighbor_chef.Models;

namespace neighbor_chef.Exceptions.Orders;

public class InvalidOrderStatusTransitionException : Exception
{
    public InvalidOrderStatusTransitionException(OrderStatus currentStatus, OrderStatus newStatus)
        : base($"Invalid order status transition from {currentStatus.ToString()} to {newStatus.ToString()}")
    {
    }
}