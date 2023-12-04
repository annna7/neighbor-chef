using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using neighbor_chef.Models.Base;
using neighbor_chef.Specifications;

namespace neighbor_chef.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        // Get by id
        Task<TEntity?> GetByIdAsync(Guid id);
        
        // Get all
        Task<List<TEntity>> GetAllAsync();

        Task<TEntity?> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

        Task<IEnumerable<TEntity>> FindWithSpecificationPatternAsync(ISpecification<TEntity> specification = null);
        Task<TEntity?> FindFirstOrDefaultWithSpecificationPatternAsync(ISpecification<TEntity> specification = null);
        
        // Add
        Task AddAsync(TEntity entity);
        Task CreateRangeAsync(IEnumerable<TEntity> entities);

        // Update
        Task UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        

        // Delete 
        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(IEnumerable<TEntity> entityIds);

        // Save
        Task<bool> SaveAsync();
    }
}