using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using neighbor_chef.Data;
using neighbor_chef.Models.Base;

namespace neighbor_chef.Repositories.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _table;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var entity = await _table.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id);
            if (entity == null)
            {
                throw new Exception($"Entity with id {id} not found.");
            }

            return entity;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _table.AsNoTracking().ToListAsync();
        }

        public Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>>? includes = null)
        {
            IQueryable<TEntity> query = _table;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return orderBy != null ? orderBy(query).FirstOrDefaultAsync() : query.FirstOrDefaultAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _table.AddAsync(entity);
        }

        public async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _table.AddRangeAsync(entities);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _table.Update(entity);
            await Task.CompletedTask;  // If you're using EF Core's Change Tracking, SaveChangesAsync will persist the update.
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _table.UpdateRange(entities);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _table.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _table.RemoveRange(entities);
            await Task.CompletedTask;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
