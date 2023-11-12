using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace neighbor_chef.Models;

public class Chef : Person
{
    public string Description { get; set; } = null!;
    
    [Range(1, int.MaxValue, ErrorMessage = "Max orders per day can't be negative")]
    public int MaxOrdersPerDay { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Advance notice days can't be negative")]
    public int AdvanceNoticeDays { get; set; }

    public string? AvailableDatesJson { get; set; }
    
    [NotMapped]
    public List<DateTime>? AvailableDates
    {
        get => JsonConvert.DeserializeObject<List<DateTime>>(AvailableDatesJson ?? "[]");
        set => AvailableDatesJson = JsonConvert.SerializeObject(value);
    }
    
    [NotMapped]
    public double Rating => ReviewsReceived.Count == 0 ? 0 : ReviewsReceived.Average(r => r.Rating);
    
    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();
    public virtual ICollection<Review> ReviewsReceived { get; set; } = new List<Review>();
    
    // public virtual ICollection<Order> OrdersReceived { get; set; } = new List<Order>();
}