using neighbor_chef.Exceptions.BaseExceptions;

namespace neighbor_chef.Exceptions.People;

public class ChefNotFoundException : NotFoundException
{
    public ChefNotFoundException() : base("Chef") { }

    public ChefNotFoundException(string message) : base("Chef", message) { }

    public ChefNotFoundException(string message, Exception innerException) : base("Chef", message, innerException) { }
}
