using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using neighbor_chef.Data;
using neighbor_chef.Models.Base;
using neighbor_chef.Specifications;

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

    public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            var entity = await _table.FirstOrDefaultAsync(entity => entity.Id == id);
            if (entity == null)
            {
                throw new Exception($"Entity with id {id} not found.");
            }

            return entity;
        }

        public async Task<TEntity?> GetByIdNoTrackingAsync(Guid id)
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

        public async Task<TEntity?> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
        {
            IQueryable<TEntity> query = _table.AsNoTracking();

            if (includes != null)
            {
                query = includes(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }
            else
            {
                return await query.FirstOrDefaultAsync();
            }
        }

        public async Task<IEnumerable<TEntity>> FindWithSpecificationPatternAsync(
            ISpecification<TEntity> specification = null)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), specification);
        }
        
        public async Task<TEntity?> FindFirstOrDefaultWithSpecificationPatternAsync(
            ISpecification<TEntity> specification = null, bool asNoTracking = false)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), specification, asNoTracking).FirstOrDefault();
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
