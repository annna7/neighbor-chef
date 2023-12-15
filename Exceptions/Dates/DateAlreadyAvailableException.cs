namespace neighbor_chef.Exceptions.Dates;

public class DateAlreadyAvailableException : Exception
{
    public DateAlreadyAvailableException() : base("Date already added for availability.") { }

    public DateAlreadyAvailableException(string message) : base(message) { }

    public DateAlreadyAvailableException(string message, Exception innerException) : base(message, innerException) { }
}