using neighbor_chef.Exceptions.BaseExceptions;

namespace neighbor_chef.Exceptions.Dates;

public class DateNotFoundException : NotFoundException
{
    public DateNotFoundException() : base("Date") { }
    public DateNotFoundException(string message) : base("Date", message) { }
    public DateNotFoundException(string message, Exception innerException) : base("Date", message, innerException) { }
}