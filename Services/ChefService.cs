using AutoMapper;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;
using neighbor_chef.UnitOfWork;

namespace neighbor_chef.Services;

public class ChefService : PersonService, IChefService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AccountService _accountService;
    private readonly IMapper _mapper;
    
    public ChefService(IUnitOfWork unitOfWork, AccountService accountService, IMapper mapper) : base(unitOfWork, accountService)
    {
        _mapper = mapper;
    }
    
    // public override async Task<Person> CreatePersonAsync(PersonRegisterDto chefDto)
    // {
    //     chefDto = (ChefRegisterDto) chefDto;
    //     var chef = await base.CreatePersonAsync(chefDto);
    //     await _accountService.RegisterUserAsync(chef.ApplicationUser);
    //     await _accountService.AssignRoleAsync(chef.ApplicationUser, "Chef");
    //
    //     var chefObject = _mapper.Map<Chef>(chefDto);
    //     
    //     // map existing person to chef
    //     _mapper.Map(chef, chefObject);
    //     
    //     await _unitOfWork.GetRepository<Person>().AddAsync(chefObject);
    //     await _unitOfWork.CompleteAsync();
    //     return chef;
    // }
}