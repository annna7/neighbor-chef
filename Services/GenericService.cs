using neighbor_chef.UnitOfWork;

namespace neighbor_chef.Services;

public class GenericService
{
    protected readonly IUnitOfWork UnitOfWork;
    
    public GenericService(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
}