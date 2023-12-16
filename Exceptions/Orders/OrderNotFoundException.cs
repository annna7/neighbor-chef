using neighbor_chef.Exceptions.BaseExceptions;

namespace neighbor_chef.Exceptions.Orders;

public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException() : base("Order") { }

    public OrderNotFoundException(string message) : base("Order", message) { }

    public OrderNotFoundException(string message, Exception innerException) : base("Order", message, innerException) { }
}