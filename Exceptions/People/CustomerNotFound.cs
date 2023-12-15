using neighbor_chef.Exceptions.BaseExceptions;

namespace neighbor_chef.Exceptions.People;

public class CustomerNotFoundException : NotFoundException
{
    public CustomerNotFoundException() : base("Customer") { }

    public CustomerNotFoundException(string message) : base("Customer", message) { }

    public CustomerNotFoundException(string message, Exception innerException) : base("Customer", message, innerException) { }
}