using System.Linq.Expressions;
using neighbor_chef.Models.Base;

namespace neighbor_chef.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        // Get by id
        Task<TEntity> GetByIdAsync(Guid id);
        
        // Get all
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>>? includes = null);
        
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