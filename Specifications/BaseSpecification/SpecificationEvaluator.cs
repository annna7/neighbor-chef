using Microsoft.EntityFrameworkCore;
using neighbor_chef.Models.Base;

namespace neighbor_chef.Specifications;

public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification, bool asNoTracking = false)
    {
        var query = inputQuery;

        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        foreach (var include in specification.Includes)
        {
            query = query.Include(include);
        }
        
        foreach (var includeQuery in specification.IncludeQueries)
        {
            query = query.Include(includeQuery.Include);
        }
        
        foreach (var include in specification.IncludeStrings)
        {
            query = query.Include(include);
        }
        
        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.GroupBy != null)
        {
            query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
        }
        
        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip)
                .Take(specification.Take);
        }
        return query;
    }
}