using System.ComponentModel.DataAnnotations.Schema;
using neighbor_chef.Models.Base;
using Newtonsoft.Json;

namespace neighbor_chef.Models;

public class Meal : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? PictureUrl { get; set; }
    public decimal Price { get; set; }
    public string? IngredientsJson { get; set; }

    [NotMapped]
    public List<string>? Ingredients
    {
        get => JsonConvert.DeserializeObject<List<string>>(IngredientsJson ?? "[]");
        set => IngredientsJson = JsonConvert.SerializeObject(value);
    }
    
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;
}