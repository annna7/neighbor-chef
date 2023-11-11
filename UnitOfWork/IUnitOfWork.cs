using Neighbor_Chef.Models.Base;
using Neighbor_Chef.Repositories.GenericRepository;

namespace Neighbor_Chef.UnitOfWork
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
