using neighbor_chef.Exceptions.BaseExceptions;

namespace neighbor_chef.Exceptions.Meals;

public class MealNotFoundException : NotFoundException
{
    public MealNotFoundException() : base("Meal") { }

    public MealNotFoundException(string message) : base("Meal", message) { }

    public MealNotFoundException(string message, Exception innerException) : base("Meal", message, innerException) { }
}