using neighbor_chef.Models.Base;

namespace neighbor_chef.Models;

public class FirebaseToken : BaseEntity
{
    public string Token { get; set; } = null!;
    public ICollection<Person> People { get; set; } = new List<Person>();
}