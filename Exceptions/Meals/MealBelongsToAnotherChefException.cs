namespace neighbor_chef.Exceptions.Meals;

public class MealBelongsToAnotherChefException : Exception
{
    public MealBelongsToAnotherChefException(Guid mealId, Guid firstChefId, Guid secondChefId) : 
        base($"Meal with id {mealId} belongs to another chef with id {firstChefId} instead of {secondChefId}." ) { }
}