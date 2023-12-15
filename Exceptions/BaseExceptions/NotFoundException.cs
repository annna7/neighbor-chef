namespace neighbor_chef.Exceptions.BaseExceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entityName) : base($"{entityName} not found.") { }

    public NotFoundException(string entityName, string message) : base($"{entityName} not found. {message}") { }

    public NotFoundException(string entityName, string message, Exception innerException) : base($"{entityName} not found. {message}", innerException) { }
}