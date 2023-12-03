using neighbor_chef.UnitOfWork;

namespace neighbor_chef.Services;

public class GenericService : IGenericService
{
    protected readonly IUnitOfWork UnitOfWork;
    
    protected GenericService(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
}