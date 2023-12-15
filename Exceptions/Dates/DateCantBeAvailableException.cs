namespace neighbor_chef.Exceptions.Dates;

public class DateCantBeAvailableException : Exception
{
    public DateCantBeAvailableException() : base("Date can't be made available!") { }

    public DateCantBeAvailableException(string message) : base(message) { }

    public DateCantBeAvailableException(string message, Exception innerException) : base(message, innerException) { }
}