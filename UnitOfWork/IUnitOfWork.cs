using neighbor_chef.Models.Base;
using neighbor_chef.Repositories.GenericRepository;

namespace neighbor_chef.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        Task<int> CompleteAsync();
    }
}
