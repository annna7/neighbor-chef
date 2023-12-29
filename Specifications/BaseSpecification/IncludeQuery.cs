using System.Linq.Expressions;

namespace neighbor_chef.Specifications;

public class IncludeQuery<T, TPreviousProperty>
{
    public Expression<Func<T, ICollection<TPreviousProperty>>> Include { get; set; }
    public Expression<Func<TPreviousProperty, object>> ThenInclude { get; set; }
}