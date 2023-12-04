using System.Linq.Expressions;

namespace neighbor_chef.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; } // For including navigation properties as string
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        Expression<Func<T, object>> GroupBy { get; } // New group by functionality
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}