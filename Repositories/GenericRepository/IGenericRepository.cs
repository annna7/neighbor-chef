using Neighbor_Chef.Models.Base;

namespace Neighbor_Chef.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        // Get by id
        Task<TEntity> GetByIdAsync(Guid id);
        
        // Get all
        Task<List<TEntity>> GetAllAsync();
        
        // Add
        Task CreateAsync(TEntity entity);
        Task CreateRangeAsync(IEnumerable<TEntity> entities);

        // Update
        Task UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        

        // Delete 
        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);

        // Save
        Task<bool> SaveAsync();
    }
}